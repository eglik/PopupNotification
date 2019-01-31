using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PopupNotification
{
    public class DirectorySync
    {
        MainWindow mainWindow;
        Dictionary<string, DateTime> currentList;

        public DirectorySync(MainWindow main)
        {
            mainWindow = main;
            currentList = mainWindow.GetDirectorys();
        }

        public Dictionary<string, DateTime> Synchronize()
        {
            string message = "";
            DirectoryInfo directoryInfo = new DirectoryInfo(DirectoryPath.Path);

            Dictionary<string, DateTime> datas = new Dictionary<string, DateTime>();

            foreach (var directory in directoryInfo.GetDirectories())
            {
                datas.Add(directory.Name, directory.LastWriteTime);
            }

            foreach (var data in datas)
            {
                // if new data
                if (!currentList.ContainsKey(data.Key))
                {
                    currentList.Add(data.Key, data.Value);
                    mainWindow.OpenPopupDialog(data.Key, PopupNotificationState.Created);
                }

                currentList.TryGetValue(data.Key, out DateTime dataTime);

                if (data.Value != dataTime)
                {
                    currentList[data.Key] = data.Value;
                    mainWindow.OpenPopupDialog(data.Key, PopupNotificationState.Changed);
                }
            }

            return currentList;
        }
    }
}