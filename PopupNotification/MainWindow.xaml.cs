using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace StalkerEye
{
    public partial class MainWindow : Window
    {
        NotifyIcon notify;
        Introduce introduce;
        List<PopupNotificationDialog> popupNotificationDialogs = new List<PopupNotificationDialog>();

        public MainWindow()
        {
            if (Properties.Settings.Default.Path == "")
            {
                OpenDirectoryDialog();
            }

            TrayInitialize();
            WatcherInitialize();
            InitializeComponent();
        }

        public void WatcherInitialize()
        {
            FileSystemWatcher watcher = new FileSystemWatcher();

            watcher.Path = Properties.Settings.Default.Path;

            watcher.NotifyFilter = NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.FileName
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.CreationTime
                                 | NotifyFilters.Size;

            watcher.Created += OnCreated;
            watcher.Deleted += OnDeleted;
            watcher.Changed += OnChanged;
            watcher.Renamed += OnRenamed;

            watcher.EnableRaisingEvents = true;
        }

        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            OpenPopupDialog(string.Format("{0} is renamed by {1}", e.OldName, e.Name));
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            OpenPopupDialog(string.Format("{0} is changed", e.Name));
        }

        private void OnDeleted(object sender, FileSystemEventArgs e)
        {
            OpenPopupDialog(string.Format("{0} is deleted", e.Name));
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            OpenPopupDialog(string.Format("{0} is created", e.Name));
        }

        private void TrayInitialize()
        {
            notify = new NotifyIcon();
            notify.Icon = Properties.Resources.folderTray;
            notify.Text = "StalkerEye";
            notify.Visible = true;

            ShowInTaskbar = false;

            System.Windows.Forms.ContextMenu contextMenu = new System.Windows.Forms.ContextMenu();
            System.Windows.Forms.MenuItem exit = contextMenu.MenuItems.Add("Exit");
            System.Windows.Forms.MenuItem line = contextMenu.MenuItems.Add("-");
            System.Windows.Forms.MenuItem about = contextMenu.MenuItems.Add("About");
            System.Windows.Forms.MenuItem change = contextMenu.MenuItems.Add("Change Directory");

            exit.Click += ExitControl;
            about.Click += About;
            change.Click += ChangeDirectory;

            notify.ContextMenu = contextMenu;

            Hide();
        }

        private void About(object sender, EventArgs e)
        {
            introduce = new Introduce();
            introduce.Show();
        }

        private void ExitControl(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
            Close();
        }

        private void ChangeDirectory(object sender, EventArgs e)
        {
            OpenDirectoryDialog();
        }

        public void OpenPopupDialog(string message)
        {
            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
            {
                PopupNotificationDialog popup;

                if (popupNotificationDialogs.Count > 0)
                {
                    for (int i = 0; i < popupNotificationDialogs.Count; i++)
                    {
                        if (popupNotificationDialogs[i].popup.Opacity == 0)
                        {
                            if (i == 0)
                            {
                                popupNotificationDialogs[i] = new PopupNotificationDialog(message, null);
                            }
                            else
                            {
                                popupNotificationDialogs[i] = new PopupNotificationDialog(message, popupNotificationDialogs[i - 1]);
                            }
                            popupNotificationDialogs[i].popup.Show();
                            return;
                        }
                    }
                    popup = new PopupNotificationDialog(message, popupNotificationDialogs[popupNotificationDialogs.Count - 1]);
                }
                else
                {
                    popup = new PopupNotificationDialog(message, null);
                }

                popupNotificationDialogs.Add(popup);
                popup.Show();
            }));
        }

        public void OpenDirectoryDialog()
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;

            var result = dialog.ShowDialog();

            if (result == CommonFileDialogResult.Ok)
            {
                Properties.Settings.Default.Path = dialog.FileName;

                DirectoryInfo directoryInfo = new DirectoryInfo(Properties.Settings.Default.Path);

                Properties.Settings.Default.Save();
            }
            else if(Properties.Settings.Default.Path == "")
            {
                System.Windows.Application.Current.Shutdown();
            }
        }
    }
}
