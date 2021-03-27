using P1;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<Pregled> Pregledi
        {
            get;
            set;
        }

        public ObservableCollection<Operacija> Operacije
        {
            get;
            set;
        }

        public LekarUI()
        {
            InitializeComponent();

            //Tred za prikazivanje sata i datuma
            DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                this.dateAndTime.Content = DateTime.Now.ToString("HH:mm │ dd/MM/yyyy");
            }, this.Dispatcher);

            //Tabela pregleda
            this.DataContext = this;
            Pregledi = new ObservableCollection<Pregled>();


            Pregled p1 = new Pregled("07/08/2021", "07/08/2021");

            //Operacija o1 = new Operacija("07/08/2021", "07/08/2021");

            Pregledi.Add(p1);
            //Operacije.Add(o1);

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

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            //Button: Zakazi pregled
            TerminCreate terminCreate = new TerminCreate();
            terminCreate.Show();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            //Button: Zakazi pregled
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            //Button: Zakazi pregled
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            //Button: Zakazi pregled
        }

        private void dataGridTermini_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
