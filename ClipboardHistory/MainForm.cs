using Microsoft.Win32;
using System.Diagnostics;
using ClipboardHistory.Properties;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace ClipboardHistory
{
    public partial class MainForm : Form
    {
        private Thread clipboardMonitoringThread;
        private bool isMonitoring = false;

        public MainForm()
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            InitializeComponent();
            StartMonitoring();
        }

        private void btn_OpenHistoryLogs_Click(object sender, EventArgs e)
        {
            string currentDirectory = Environment.CurrentDirectory;

            try
            {
                // Open the current directory in File Explorer
                Process.Start(new ProcessStartInfo
                {
                    FileName = currentDirectory,
                    UseShellExecute = true // Required to open the folder
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while opening the folder: {ex.Message}");
            }
        }

        private void SetStartup(bool enable)
        {
            const string appName = "Clipboard History";
            string exePath = Application.ExecutablePath;

            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);

            if (enable)
            {
                // Add the application to startup
                registryKey.SetValue(appName, exePath);
            }
            else
            {
                // Remove the application from startup
                registryKey.DeleteValue(appName, false);
            }
        }

        private void cb_LaunchWithWindows_Click(object sender, EventArgs e)
        {
            if (cb_LaunchWithWindows.Checked)
            {
                SetStartup(true);
                Settings.Default.startup = true;
                Settings.Default.Save();
            }
            else
            {
                SetStartup(false);
                Settings.Default.startup = false;
                Settings.Default.Save();
            }
        }

        private void StartMonitoring()
        {
            if (!isMonitoring)
            {
                isMonitoring = true;
                clipboardMonitoringThread = new Thread(MonitorClipboard)
                {
                    IsBackground = true
                };
                clipboardMonitoringThread.SetApartmentState(ApartmentState.STA);
                clipboardMonitoringThread.Start();
            }
        }

        private void StopMonitoring()
        {
            if (isMonitoring)
            {
                isMonitoring = false;
                clipboardMonitoringThread.Join(); // Wait for the thread to finish
            }
        }

        private void MonitorClipboard()
        {
            string logFilePath = Path.Combine(Environment.CurrentDirectory, "logs.txt");
            string imagesFolderPath = Path.Combine(Environment.CurrentDirectory, "Images");

            // Ensure the log file and images folder exist
            if (!File.Exists(logFilePath))
            {
                File.Create(logFilePath).Close();
            }
            if (!Directory.Exists(imagesFolderPath))
            {
                Directory.CreateDirectory(imagesFolderPath);
            }

            string lastCopiedText = string.Empty;
            Image lastCopiedImage = null;

            while (isMonitoring)
            {
                try
                {
                    // Check if clipboard contains text
                    if (Clipboard.ContainsText())
                    {
                        string currentText = Clipboard.GetText();
                        if (currentText != lastCopiedText)
                        {
                            lastCopiedText = currentText;
                            string logEntry = $"[{DateTime.Now:dd.MM.yyyy - HH:mm:ss}]{Environment.NewLine}{currentText}{Environment.NewLine}";
                            File.AppendAllText(logFilePath, logEntry);
                        }
                    }
                    // Check if clipboard contains an image
                    else if (Clipboard.ContainsImage())
                    {
                        Image currentImage = Clipboard.GetImage();
                        if (currentImage != null && !ImagesAreEqual(currentImage, lastCopiedImage))
                        {
                            lastCopiedImage = currentImage;

                            // Save the image
                            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                            string imageFilePath = Path.Combine(imagesFolderPath, $"Image_{timestamp}.png");
                            currentImage.Save(imageFilePath);

                            // Log the image save action
                            string logEntry = $"[{DateTime.Now:dd.MM.yyyy - HH:mm:ss}]{Environment.NewLine}Image saved as 'Image_{ timestamp}.png'{Environment.NewLine}";
                            File.AppendAllText(logFilePath, logEntry);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle clipboard access errors
                    Debug.WriteLine($"Error accessing clipboard: {ex.Message}");
                }

                Thread.Sleep(500); // Polling interval
            }
        }

        // Utility method to compare images to avoid duplicates
        private bool ImagesAreEqual(Image img1, Image img2)
        {
            if (img1 == null || img2 == null)
                return false;

            // Compare image sizes as a simple way to detect differences
            return img1.Width == img2.Width && img1.Height == img2.Height;
        }


        private void btn_Exit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
