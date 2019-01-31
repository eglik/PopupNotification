using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace PopupNotification
{
    public class DirectoryThread
    {
        Thread thread;
        MainWindow mainWindow;

        DirectorySync sync;

        public DirectoryThread(MainWindow main)
        {
            mainWindow = main;
            sync = new DirectorySync(main);
            thread = new Thread(new ThreadStart(Run));
        }

        private void Run()
        {
            while(true)
            {
                Thread.Sleep(1000);
                mainWindow.SetDirectorys(sync.Synchronize());
            }
        }

        public void Start() => thread.Start();
        public void Abort() => thread.Abort();
    }
}
