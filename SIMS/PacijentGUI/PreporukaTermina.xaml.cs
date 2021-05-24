using SIMS.Repositories.SecretaryRepo;
using SIMS.Repositories.AppointmentRepo;
using SIMS.Repositories.DoctorRepo;
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
using SIMS.Model;
using SIMS.Service;

namespace SIMS.PacijentGUI
{
    /// <summary>
    /// Interaction logic for PreporukaTermina.xaml
    /// </summary>
    /// 

    class TerminZaPreporuku
    {
        private List<String> idLekara;
        private DateTime vrijeme;
        public TerminZaPreporuku()
        {

        }
        public TerminZaPreporuku(DateTime vrijeme)
        {
            this.vrijeme = vrijeme;
            this.idLekara = new DoctorFileRepository().GetAllId();
        }

        public List<string> IdLekara { get => idLekara; set => idLekara = value; }
        public DateTime Vrijeme { get => vrijeme; set => vrijeme = value; }
    }

    public partial class PreporukaTermina : UserControl
    {
        Patient pacijent;
        List<Appointment> termini;
        List<Appointment> preporuceniTermini;
        List<TerminZaPreporuku> terminZaPreporuku;
        List<Doctor> lekari;
        public PreporukaTermina(Patient p)
        {
            InitializeComponent();
            pacijent = p;
            termini = new AppointmentFileRepository().GetAll();
            preporuceniTermini = new List<Appointment>();
            //ListaDoktora.IsHitTestVisible = false;
            terminZaPreporuku = new List<TerminZaPreporuku>();
        
            lekari = new DoctorFileRepository().GetAll();
            this.DataContext = this;
        }

        private void preporuka()
        {

            if (lekarChecked.IsChecked == true)
            {
                RecommendationService recommendationService = new RecommendationService(TypeOfRecommendation.DoctorRecommendation, lekari[ListaDoktora.SelectedIndex].Jmbg, (DateTime)PocetniDatum.SelectedDate, (DateTime)KrajnjiDatum.SelectedDate, PocetnaStranica.getInstance().Pacijent.Jmbg);
                preporuceniTermini = recommendationService.GetRecommendedAppointments();
            }
            else
            {
                RecommendationService recommendationService = new RecommendationService(TypeOfRecommendation.DateRecommendation, "", (DateTime)PocetniDatum.SelectedDate, (DateTime)KrajnjiDatum.SelectedDate, PocetnaStranica.getInstance().Pacijent.Jmbg);
                preporuceniTermini = recommendationService.GetRecommendedAppointments();
            }

            
        }
        public Patient Pacijent { get => pacijent; set => pacijent = value; }
        public List<Doctor> Lekari { get => lekari; set => lekari = value; }

        private bool validiraj()
        {
            if (PocetniDatum.SelectedDate == null || KrajnjiDatum.SelectedDate == null )
            {
                MessageBox.Show("Molimo vas da popunite sva polja");
                return false;
            }
            if (PocetniDatum.SelectedDate.Value > KrajnjiDatum.SelectedDate.Value)
            {
                MessageBox.Show("Pocetni datum treba da bude manji od krajnjeg datuma");
                return false;
            }
            
            if (lekarChecked.IsChecked==false && datumChecked.IsChecked == false)
            {
                MessageBox.Show("Molimo vas da popunite vas prioritet");
                return false;
            }
            if (lekarChecked.IsChecked==true && ListaDoktora.SelectedItem == null)
            {
                MessageBox.Show("Potrebno je da izaberete prioritetnog doktora");
                return false;
            }
            return true;
            
        }
        private void Trazi_Click(object sender, RoutedEventArgs e)
        {
            if (!validiraj())
            {
                return;
            }
            preporuka();
            PreporuceniTermini preporuceni = new PreporuceniTermini();
            preporuceni.dodajTermine(preporuceniTermini);
            PocetnaStranica.getInstance().frame.Content = preporuceni;
        }

        private void PocetniDatum_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime oznaceniDatum = (DateTime)PocetniDatum.SelectedDate;
            CalendarDateRange cdr = new CalendarDateRange(DateTime.MinValue,oznaceniDatum );
            CalendarDateRange cdr1 = new CalendarDateRange(oznaceniDatum.AddDays(7), DateTime.MaxValue);
            KrajnjiDatum.BlackoutDates.Add(cdr);
            KrajnjiDatum.BlackoutDates.Add(cdr1);
        }

        private void datumChecked_Checked(object sender, RoutedEventArgs e)
        {
            ListaDoktora.IsHitTestVisible = false;
            ListaDoktora.SelectedIndex = -1;
        }

        private void lekarChecked_Checked(object sender, RoutedEventArgs e)
        {
            ListaDoktora.IsHitTestVisible = true;
        }

        private void KrajnjiDatum_CalendarOpened(object sender, RoutedEventArgs e)
        {
            if (PocetniDatum.SelectedDate == null)
            {
                MessageBox.Show("Potrebno je da izaberete pocetni datum");
                return;
            }
        }
    }
}
