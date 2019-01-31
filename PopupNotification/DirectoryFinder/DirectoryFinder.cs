using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace PopupNotification
{
    public class DirectoryFinder
    {
        static List<PopupNotificationDialog> popupNotificationDialogs = new List<PopupNotificationDialog>();

        static string message = "";

        static Thread thread = new Thread(new ParameterizedThreadStart(Updater));

        public static void Run(Dictionary<string, DateTime> list)
        {
            thread.Start(list);
        }

        public static void Close()
        {
            thread.Abort();
        }

        private static void Updater(object list)
        {
            
        }

        private static void Show(string data)
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
                            popupNotificationDialogs[i] = new PopupNotificationDialog(data, null);
                        }
                        else
                        {
                            popupNotificationDialogs[i] = new PopupNotificationDialog(data, popupNotificationDialogs[i - 1]);
                        }
                        popupNotificationDialogs[i].popup.Show();
                        return;
                    }
                    //MessageBox.Show(popupNotificationDialogs[i].popup.Opacity.ToString());
                }
                popup = new PopupNotificationDialog(data, popupNotificationDialogs[popupNotificationDialogs.Count - 1]);
            }
            else
            {
                popup = new PopupNotificationDialog(data, null);
            }

            popupNotificationDialogs.Add(popup);
            popup.Show();
        }
    }
}
