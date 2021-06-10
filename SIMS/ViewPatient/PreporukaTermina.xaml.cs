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
using SIMS.DTO;
using SIMS.Model;
using SIMS.Service.RecommendationAppointmentService;
using System.Threading.Tasks;
using SIMS.Controller;

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

            PocetniDatum.DisplayDateStart = DateTime.Now.AddDays(1);
            CalendarDateRange cdr = new CalendarDateRange(DateTime.MinValue, DateTime.Now);
            PocetniDatum.BlackoutDates.Add(cdr);

            KrajnjiDatum.DisplayDateStart = DateTime.Now.AddDays(1);
            
            KrajnjiDatum.BlackoutDates.Add(cdr);

            lekari = new DoctorFileRepository().GetAll();
            this.DataContext = this;
        }

        private void preporuka()
        {
            

            if (lekarChecked.IsChecked == true)
            {
                RecommendedAppointmentDTO recommendedAppointmentDTO = new RecommendedAppointmentDTO(
                    TypeOfRecommendation.DoctorRecommendation, lekari[ListaDoktora.SelectedIndex].Jmbg,
                    (DateTime)PocetniDatum.SelectedDate, (DateTime)KrajnjiDatum.SelectedDate,
                    PocetnaStranica.getInstance().Pacijent.Jmbg);
                RecommendedAppointmentController recommendationController = new RecommendedAppointmentController();
                preporuceniTermini = recommendationController.DoctorRecommendataion(recommendedAppointmentDTO);
            }
            else
            {
                RecommendedAppointmentDTO recommendedAppointmentDTO = new RecommendedAppointmentDTO(
                    TypeOfRecommendation.DoctorRecommendation,"",
                    (DateTime)PocetniDatum.SelectedDate, (DateTime)KrajnjiDatum.SelectedDate,
                    PocetnaStranica.getInstance().Pacijent.Jmbg);
                RecommendedAppointmentController recommendationController = new RecommendedAppointmentController();
                preporuceniTermini = recommendationController.DateRecommendation(recommendedAppointmentDTO);
                
            }

            
        }
        public Patient Pacijent { get => pacijent; set => pacijent = value; }
        public List<Doctor> Lekari { get => lekari; set => lekari = value; }

        private void ispisiPoruku(String text)
        {
            Poruka.Visibility = Visibility.Visible;
            Poruka.Content = text;
            Task.Delay(3000).ContinueWith(_ =>
            {
                Application.Current.Dispatcher.Invoke(
                    System.Windows.Threading.DispatcherPriority.Normal,
                    new Action(
                        delegate ()
                        {
                            Poruka.Visibility = Visibility.Collapsed;
                        }
                    ));
            });
        }
        private bool validiraj()
        {
            if (PocetniDatum.SelectedDate == null || KrajnjiDatum.SelectedDate == null )
            {
                ispisiPoruku("Sva polja trebaju biti popunjena");
                return false;
            }
            if (PocetniDatum.SelectedDate.Value > KrajnjiDatum.SelectedDate.Value)
            {
                ispisiPoruku("Pocetni datum treba biti manji od krajnjeg");
                return false;
            }
            
            if (lekarChecked.IsChecked==false && datumChecked.IsChecked == false)
            {
                ispisiPoruku("Molimo Vas da izaberete prioritet");
                return false;
            }
            if (lekarChecked.IsChecked==true && ListaDoktora.SelectedItem == null)
            {
                ispisiPoruku("Potrebno je da izaberete prioritetnog doktora");
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
            if (oznaceniDatum > KrajnjiDatum.SelectedDate)
            {
                PocetniDatum.BorderBrush = Brushes.Red;
                MaterialDesignThemes.Wpf.HintAssist.SetHelperText(PocetniDatum, "Datum mora biti manji od krajnjeg");
                
            }
            else
            {
                MaterialDesignThemes.Wpf.HintAssist.SetHelperText(PocetniDatum, "");
                PocetniDatum.BorderBrush = Brushes.DarkGray;
                KrajnjiDatum.BorderBrush = Brushes.DarkGray;
                MaterialDesignThemes.Wpf.HintAssist.SetHelperText(KrajnjiDatum, "");
            }
            
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
            if (PocetniDatum.SelectedDate > KrajnjiDatum.SelectedDate)
            {
                KrajnjiDatum.BorderBrush = Brushes.Red;
                MaterialDesignThemes.Wpf.HintAssist.SetHelperText(KrajnjiDatum,"Datum mora biti veći od početnog");
                MaterialDesignThemes.Wpf.HintAssist.SetBackground(KrajnjiDatum, Brushes.Red);
                return;
            }
            else
            {
                KrajnjiDatum.BorderBrush = Brushes.DarkGray;
                MaterialDesignThemes.Wpf.HintAssist.SetHelperText(KrajnjiDatum, "");
                PocetniDatum.BorderBrush = Brushes.DarkGray;
                MaterialDesignThemes.Wpf.HintAssist.SetHelperText(PocetniDatum, "");
            }
        }
    }
}
