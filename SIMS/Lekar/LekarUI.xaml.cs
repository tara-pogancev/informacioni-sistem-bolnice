
using Model;
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
        public ObservableCollection<Termin> Termini
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
            Termini = new ObservableCollection<Termin>();

            TerminStorage terminStorage = new TerminStorage();

            DateTime tempDate = new DateTime(2020, 3, 4, 8, 0, 15);
            TimeSpan tempSpan = new TimeSpan(0, 30, 0);

            Drzava SrbijaT = new Drzava("Srbija");
            Grad BP = new Grad("Backa Palanka", 15000, SrbijaT);
            Adresa adresaT = new Adresa("Vojvode Putnika", 1, BP);
            UlogovanKorisnik TaraP = new UlogovanKorisnik("Tara", "Pogancev", "1234567891021", "doktor", "doktor", "tara123@gmail.com", "0645568131", adresaT);

            Lekar l = new Lekar(TaraP.Ime, TaraP.Prezime, TaraP.Jmbg, TaraP.KorisnickoIme, TaraP.Lozinka, TaraP.Email, TaraP.Telefon, TaraP.Adresa, 15);
            Pacijent p = new Pacijent(TaraP.Ime, TaraP.Prezime, TaraP.Jmbg, TaraP.KorisnickoIme, TaraP.Lozinka, TaraP.Email, TaraP.Telefon, TaraP.Adresa, "00777000", false);

            Prostorija prostorija = new Prostorija(adresaT, 2, 22, true, TipProstorije.zaPreglede, "E221");
            Termin termin = new Termin(tempDate, tempSpan, TipTermina.pregled, l, p, prostorija);

            TerminStorage storage = new TerminStorage();
            storage.Create(termin);

            Termini.Add(termin);


            //List<Termin> terminiByLekar = terminStorage.ReadByLekar(l);

            // foreach (Termin t in terminiByLekar)
            //     Termini.Add(t);



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
            //Button: Zakazi operaciju
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            //Button: Uredi termin
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            //Button: Otkaži pregled
            Termin toDelete = (Termin)dataGridTermini.SelectedItem;

            if (MessageBox.Show("Da li ste sigurni da želite da otkažete termin?", 
                "Otkaži termin", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {

                //TODO: Otkazi termin

                MessageBox.Show("Termin je uspešno otkazan!");
            }


        }

        private void dataGridTermini_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
