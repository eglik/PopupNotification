﻿using System;
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

        public DirectoryThread(MainWindow main)
        {
            mainWindow = main;
            thread = new Thread(new ThreadStart(Run));
        }

        private void Run()
        {
            while(true)
            {
                Thread.Sleep(1000);
                mainWindow.OpenPopupDialog("TESTING");
            }
        }

        public void Start() => thread.Start();
        public void Abort() => thread.Abort();
    }
}
