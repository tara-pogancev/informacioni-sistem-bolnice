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

namespace SIMS.UpravnikGUI
{
    /// <summary>
    /// Interaction logic for UpravnikUI.xaml
    /// </summary>
    public partial class UpravnikUI : Window
    {
        public UpravnikUI()
        {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                this.dateAndTime.Content = DateTime.Now.ToString("HH:mm │ dd.MM.yyyy.");
            }, this.Dispatcher);
        }

        private void Kreiraj_Prostoriju_Click(object sender, RoutedEventArgs e)
        {
            ProstorijaCreate prostorijaCreate = new ProstorijaCreate();
            prostorijaCreate.Show();
        }

        private void Pregledaj_Uredi_Prostoriju_Click(object sender, RoutedEventArgs e)
        {
            ProstorijaUpdate prostorijaUpdate = new ProstorijaUpdate();
            prostorijaUpdate.Show();
        }

        private void Button_Log_Out(object sender, RoutedEventArgs e)
        {

            new MainWindow().Show();
            this.Close();
        }

    }
}
