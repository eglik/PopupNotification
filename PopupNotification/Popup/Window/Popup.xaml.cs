using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PopupNotification
{
    public partial class Popup : Window
    {
        private Thread thread;
        PopupNotificationDialog popupDialog;

        public Popup()
        {
            InitializeComponent();
            thread = new Thread(new ThreadStart(CloseFadein));
            thread.Start();
        }

        public void SetDialog(PopupNotificationDialog dialog)
        {
            popupDialog = dialog;
        }

        public void StartCloseAnimation()
        {
            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
            {
                Storyboard storyboard = new Storyboard();

                DoubleAnimation animation = new DoubleAnimation(1, 0, new TimeSpan(0, 0, 0, 0, 500));

                animation.Completed += AnimationCompleted;

                storyboard.Children.Add(animation);

                Storyboard.SetTarget(animation, popup);
                Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));
                storyboard.Begin();
            }));
        }

        public void CloseFadein()
        {
            Thread.Sleep(5000);
            StartCloseAnimation();
        }

        private void AnimationCompleted(object sender, EventArgs e)
        {
            Close();
        }
    }
}