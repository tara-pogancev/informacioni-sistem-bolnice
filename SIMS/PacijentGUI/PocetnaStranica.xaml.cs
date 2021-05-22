using SIMS.Repositories.PatientRepo;
using SIMS.Model;
using SIMS.Repositories.AppointmentRepo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SIMS.PacijentGUI
{
    /// <summary>
    /// Interaction logic for PocetnaStranica.xaml
    /// </summary>
    public partial class PocetnaStranica : Window
    {
        private Patient pacijent;
        private static PocetnaStranica instance=null;
        public static PocetnaStranica getInstance()
        {
            if (instance == null)
            {
                instance = new PocetnaStranica();
            }
            return instance;
        }
        
        private PocetnaStranica()
        {
            
            InitializeComponent();
            Tabovi.Content = new PocetniEkran(pacijent);
            this.DataContext = this;
            
            
            
        }

        public void kreirajAnketu()
        {
            List<HospitalSurvey> anketeBolnice = new HospitalSurveyFileRepository().GetPatientSurveys(pacijent);
            if (anketeBolnice.Count == 0)
            {
                Anketa.Visibility = Visibility.Visible;
                return;
            }
            if (Math.Abs(anketeBolnice[anketeBolnice.Count-1].TrenutniBrojPregleda - brojZavrsenihPRegleda()) > 5)
            {
                Anketa.Visibility = Visibility.Visible;
            }
            else if (anketeBolnice[0].DatumKreiranjaAnkete.AddMonths(3) < DateTime.Now)
            {
                Anketa.Visibility = Visibility.Visible;
            }
            
        }

        private int brojZavrsenihPRegleda()
        {
            List<Appointment> zakazaniTermini = new AppointmentFileRepository().GetPatientAppointments(pacijent);
            int brojacZavrsenihPregleda = 0;
            foreach(Appointment termin in zakazaniTermini)
            {
                if (termin.IsPast)
                {
                    brojacZavrsenihPregleda++;
                }
            }
            return brojacZavrsenihPregleda;
        }

        public void pokreniNit()
        {
            Thread provjeraPredstojecihTermina = new Thread(new ThreadStart(notifikacijaZaTermine));
            provjeraPredstojecihTermina.IsBackground = true;
            provjeraPredstojecihTermina.Start();
            
        }
        private void notifikacijaZaTermine()
        {
            while (true)
            {
                if (postojeTermini())
                {
                    this.Dispatcher.Invoke(() => {
                        ObavjestenjeOTerminu obavjestenjeOTerminu = new ObavjestenjeOTerminu();
                        obavjestenjeOTerminu.ShowDialog();
                    });
                }
                Thread.Sleep(1000 * 60 * 60);
            }
        }

        private bool postojeTermini()
        {
            List<Appointment> zakazaniTermini = new AppointmentFileRepository().GetPatientAppointments(pacijent);
            foreach(Appointment termin in zakazaniTermini)
            {
                if (DateTime.Now <= termin.PocetnoVreme && DateTime.Now.AddMinutes(60)>=termin.PocetnoVreme)
                {
                    return true;
                }

            }
            return false;

        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListViewMenu.SelectedIndex;


            switch (index)
            {
                case 0:
                    Tabovi.Content=new PocetniEkran(pacijent);
                    break;
                case 1:
                    ZakazivanjeTermina zakazivanje = ZakazivanjeTermina.getInstance();
                    zakazivanje.Pacijent = pacijent;
                    Tabovi.Content = zakazivanje;
                    break;
                case 2:
                    Tabovi.Content = new MojiTermini(pacijent);
                    break;
                case 3:
                    Tabovi.Content = new TerapijaPacijentaView();
                    break;
                case 4:
                    Tabovi.Content = new IzborLjekara();
                    break;
                case 5:
                    Tabovi.Content = new IstorijaPregleda();
                    break;
                default:
                    break;
            }
        }

        public Patient Pacijent { get => pacijent; set => pacijent = value; }

        private void Iskljucivanje_Click(object sender, RoutedEventArgs e)
        {
            
            new MainWindow().Show();
            instance = null;
            this.Close();

        }

        private void Zvonce_Click(object sender, RoutedEventArgs e)
        {
            Tabovi.Content = new Obavjestenja();
        }

        private void ListViewMenu_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            int index = ListViewMenu.SelectedIndex;


            switch (index)
            {
                case 0:
                    Tabovi.Content = new PocetniEkran(pacijent);
                    break;
                case 1:
                    ZakazivanjeTermina zakazivanje = ZakazivanjeTermina.getInstance();
                    zakazivanje.Pacijent = pacijent;
                    Tabovi.Content = zakazivanje;
                    break;
                case 2:
                    Tabovi.Content = new MojiTermini(pacijent);
                    break;
                case 3:
                    Tabovi.Content = new TerapijaPacijentaView();
                    break;
                case 4:
                    Tabovi.Content = new IzborLjekara();
                    break;
                case 5:
                    Tabovi.Content = new IstorijaPregleda();
                    break;
                default:
                    break;
            }
        }

        private void Profil_Click(object sender, RoutedEventArgs e)
        {
            Tabovi.Content = new KorisnickiProfil();
        }

        public void SetInstance()
        {
            instance = null;
        }

        private void Anketa_Click(object sender, RoutedEventArgs e)
        {
            Tabovi.Content = new AnketaBolnicePage();
        }
    }
}
