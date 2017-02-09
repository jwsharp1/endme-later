using System.ComponentModel;
using System.Windows;

namespace EndMe_Later
{
    public partial class App : Application
    {
        private System.Windows.Forms.NotifyIcon notif;
        private bool reallyQuit;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MainWindow = new MainWindow();
            MainWindow.Closing += MainWindow_Closing;

            notif = new System.Windows.Forms.NotifyIcon();
            notif.DoubleClick += (s, args) => ShowMainWindow();
            notif.Icon = EndMe_Later.Properties.Resources.fuckin_clock;
            notif.Visible = true;

            CreateContextMenu();
        }

        private void CreateContextMenu()
        {
            notif.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            notif.ContextMenuStrip.Items.Add("Open EndMe Later").Click += (s, e) => ShowMainWindow();
            notif.ContextMenuStrip.Items.Add("Quit").Click += (s, e) => ExitApplication();
        }

        private void ExitApplication()
        {
            reallyQuit = true;
            MainWindow.Close();
            notif.Dispose();
            notif = null;
        }

        private void ShowMainWindow()
        {
            if (MainWindow.IsVisible)
            {
                if (MainWindow.WindowState == WindowState.Minimized)
                {
                    MainWindow.WindowState = WindowState.Normal;
                }
                MainWindow.Activate();
            }
            else
            {
                MainWindow.Show();
            }
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            if (!reallyQuit)
            {
                e.Cancel = true;    // 
                MainWindow.Hide(); // A hidden window can be shown again, a closed one not
            }
        }
    }
}
