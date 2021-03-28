
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        public static LekarUI instance = null;
        private TerminStorage storageT = new TerminStorage();
        //private Lekar lekar;

        private ObservableCollection<Termin> termini;
        public ObservableCollection<Termin> Termini { get => termini; set => termini = value; }

        public static LekarUI getInstance()
        {
            if (instance == null)
            {
                instance = new LekarUI();
            }
            return instance;
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
            termini = new ObservableCollection<Termin>(storageT.Read());

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
            //Button: Nalog, DEBUG

        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            //Button: Zakazi pregled
            TerminCreate terminCreate = new TerminCreate();
            terminCreate.Show();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            //Button: Zakazi operaciju
            OperacijaCreate operacijaCreate = new OperacijaCreate();
            operacijaCreate.Show();
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            //Button: Uredi termin
            if (dataGridTermini.SelectedItem != null)
            {
                TerminUpdate terminUpdate = new TerminUpdate((Termin)dataGridTermini.SelectedItem);
                terminUpdate.Show();
            }

        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            //Button: Otkaži pregled

            if (dataGridTermini.SelectedItem != null) 
            {

                if (MessageBox.Show("Da li ste sigurni da želite da otkažete termin?",
                "Otkaži termin", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {                    
                     termini.Remove((Termin)dataGridTermini.SelectedItem);
                     MessageBox.Show("Termin je uspešno otkazan!");
                }
                
            }
        }

        public void dodajTermin(Termin termin)
        {
            termini.Add(termin);
            MessageBox.Show("Termin uspešno zakazan.");
        }

        private void LekarUI_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            storageT.Create(termini.ToList());
        }
    }
}
