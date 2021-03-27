
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
        public static LekarUI instance = null;
        private TerminStorage storageT = new TerminStorage();
        private Lekar lekar;

        public ObservableCollection<Termin> Termini
        {
            get;
            set;
        }

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
            Termini = new ObservableCollection<Termin>();

            this.updateTermini();

            //this.initData();
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
            MessageBox.Show("Trenutno je aktivno: " + storageT.getTerminStorage().Count + " termina.");
            this.updateTermini();

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
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            //Button: Otkaži pregled

            if (MessageBox.Show("Da li ste sigurni da želite da otkažete termin?", 
                "Otkaži termin", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                //TODO: Otkazi termin
                Termin toDelete = (Termin)dataGridTermini.SelectedItem;

                bool success = storageT.Delete(toDelete);
                if (success) 
                {
                    updateTermini();
                    MessageBox.Show("Termin je uspešno otkazan!"); 
                }
                
                else 
                { 
                    MessageBox.Show("Greška! Termin nije uspešno otkazan!"); 
                }
            }
        }

        private void updateTermini()
        {
            //List<Termin> terminiByLekar = storageT.ReadByLekar(this.lekar);
            //TODO: Staviti da prikazuje samo pojedinog lekara!

            List<Termin> terminiByLekar = storageT.getTerminStorage();

            Termini.Clear();
            foreach (Termin t in terminiByLekar)
                Termini.Add(t);
        }

        private void initData()
        {
            //Temp metoda za inicijalizovanje podataka
            DateTime tempDate = new DateTime(2020, 3, 4, 8, 0, 15);
            TimeSpan tempSpan = new TimeSpan(0, 30, 0);

            Drzava SrbijaT = new Drzava("Srbija");
            Grad BP = new Grad("Backa Palanka", 15000, SrbijaT);
            Adresa adresaT = new Adresa("Vojvode Putnika", 1, BP);
            UlogovanKorisnik TaraP = new UlogovanKorisnik("Tara", "Pogancev", "1234567891021", "doktor", "doktor", "tara123@gmail.com", "0645568131", adresaT);

            this.lekar = new Lekar(TaraP.Ime, TaraP.Prezime, TaraP.Jmbg, TaraP.KorisnickoIme, TaraP.Lozinka, TaraP.Email, TaraP.Telefon, TaraP.Adresa, 15);
            Pacijent p = new Pacijent(TaraP.Ime, TaraP.Prezime, TaraP.Jmbg, TaraP.KorisnickoIme, TaraP.Lozinka, TaraP.Email, TaraP.Telefon, TaraP.Adresa, "00777000", false);

            Prostorija prostorija = new Prostorija(adresaT, 2, 22, true, TipProstorije.zaPreglede);
            Termin termin = new Termin(tempDate, tempSpan, TipTermina.pregled, this.lekar, p, prostorija);

            //Stavljanje termina u storage

            //storageT.Create(termin);

            this.updateTermini();

        }

        public void dodajTermin(Termin termin)
        {
            storageT.AddTermin(termin);
            MessageBox.Show("Termin uspešno zakazan.");
            this.updateTermini();
        }

    }
}
