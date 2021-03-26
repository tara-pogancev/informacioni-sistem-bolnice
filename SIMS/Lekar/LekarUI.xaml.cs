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
using System.Windows.Shapes;

using System.Windows.Threading;

namespace SIMS
{
    /// <summary>
    /// Interaction logic for LekarUI.xaml
    /// </summary>
    public partial class LekarUI : Window
    {
        public LekarUI()
        {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                this.dateAndTime.Content = DateTime.Now.ToString("HH:mm │ dd/MM/yyyy");
            }, this.Dispatcher);


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Button: Termini
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Button: Pacijenti
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //Button: Istorija pregleda
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //Button: Evidentiranje materijala
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            //Button: Nalog
        }
    }
}
