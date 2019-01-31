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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PopupNotification
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<string, DateTime> directorys = new Dictionary<string, DateTime>();
        List<PopupNotificationDialog> popupNotificationDialogs = new List<PopupNotificationDialog>();
        DirectoryThread directoryThread;

        public MainWindow()
        {
            InitializeComponent();
            directoryThread = new DirectoryThread(this);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            directoryThread.Start();
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
                        //MessageBox.Show(popupNotificationDialogs[i].popup.Opacity.ToString());
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

        public void SetDictionary(Dictionary<string, DateTime> list) => directorys = list;

        public string folderPath;

        public void OpenDirectoryDialog()
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                folderPath = dialog.FileName;

                DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);

                foreach (var directory in directoryInfo.GetDirectories())
                {
                    directorys.Add(directory.Name, directory.LastWriteTime);
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            directoryThread.Abort();
        }
    }
}
