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
        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            LekarUI.GetInstance().ChangeWindowClose();
        }
    }
}
