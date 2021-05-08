using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SIMS.LekarGUI
{
    /// <summary>
    /// Interaction logic for WindowBar.xaml
    /// </summary>
    public partial class WindowBar : Page
    {
        // W I P
        

        public WindowBar()
        {
            InitializeComponent();
        }

        private void Button_Minimize(object sender, RoutedEventArgs e)
        {
            LekarUI.GetInstance().ChangeWindowMinimize();
        }

        private void Button_Size(object sender, RoutedEventArgs e)
        {
            LekarUI.GetInstance().ChangeWindowSize();

            ChangeMinimizeButton();

        }

        public void ChangeMinimizeButton()
        {
            if (LekarUI.GetInstance().GetWindowState() == WindowState.Maximized)
            {
                SizeImg.Source = new BitmapImage(new Uri(@"/src/small_window.png", UriKind.RelativeOrAbsolute));
            }

            if (LekarUI.GetInstance().GetWindowState() == WindowState.Normal)
            {
                SizeImg.Source = new BitmapImage(new Uri(@"/src/max_window.png", UriKind.RelativeOrAbsolute));
            }
        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            for (int intCounter = App.Current.Windows.Count - 1; intCounter >= 0; intCounter--)
               App.Current.Windows[intCounter].Close();

            LekarUI.GetInstance().ChangeWindowClose();
        }
    }
}
