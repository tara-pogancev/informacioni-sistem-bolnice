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
            this.idLekara = new DoctorFileRepository().getAllId();
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

        private void filtrirajTermine()
        {
            DateTime datum1 = (DateTime)PocetniDatum.SelectedDate;
            DateTime datum2 = (DateTime)KrajnjiDatum.SelectedDate;
            TimeSpan ts = new TimeSpan(8, 0, 0);
            TimeSpan ts1 = new TimeSpan(9, 0, 0);
            TimeSpan ts2 = new TimeSpan(10, 0, 0);
            while (datum1 <= datum2)
            {

   
                TerminZaPreporuku terminZaPreporuku1 = new TerminZaPreporuku(datum1 + ts);
                TerminZaPreporuku terminZaPreporuku2 = new TerminZaPreporuku(datum1 + ts1);
                TerminZaPreporuku terminZaPreporuku3 = new TerminZaPreporuku(datum1 + ts2);
                terminZaPreporuku.Add(terminZaPreporuku1);
                terminZaPreporuku.Add(terminZaPreporuku2);
                terminZaPreporuku.Add(terminZaPreporuku3);
                datum1 = datum1.AddDays(1);
            }
        }

        private void izbaciZauzeteTermine(Appointment termin)
        {
            for(int i= 0;i < terminZaPreporuku.Count;i++)
            {
                if (terminZaPreporuku[i].Vrijeme.Equals(termin.StartTime) && termin.Doctor.Jmbg.Equals(lekari[ListaDoktora.SelectedIndex].Jmbg))
                {
                    terminZaPreporuku.RemoveAt(i);
                    i--;
                }
            }
        }
        private void preporukaZaDoktora()
        {
            if (lekarChecked.IsChecked == true)
            {
                foreach (Appointment ter in termini)
                {
                    izbaciZauzeteTermine(ter);
                }
            }
            for (int i = 0; i < terminZaPreporuku.Count; i++)
            {
                Appointment termin = new Appointment();
                termin.StartTime = terminZaPreporuku[i].Vrijeme;
                termin.InitialTime = termin.StartTime;
                termin.Duration = 30;
                termin.Type = AppointmentType.examination;
                termin.Doctor = lekari[ListaDoktora.SelectedIndex];
                termin.Patient = PocetnaStranica.getInstance().Pacijent;
                termin.Room = new Room("1",RoomType.zaPreglede);
                termin.AppointmentID = DateTime.Now.ToString("yyMMddhhmmss");
                preporuceniTermini.Add(termin);
                if (i == 4)
                {
                    break;
                }
            }
        }

        private void izbaciZauzeteDoktore(Appointment termin)
        {

            for (int i = 0; i < terminZaPreporuku.Count; i++)
            {
                
                if (terminZaPreporuku[i].Vrijeme.Equals(termin.StartTime))
                {
                    terminZaPreporuku[i].IdLekara.Remove(termin.Doctor.Jmbg);
                    break;
                }
                
            }
        }
        private void izbaciPacijentoveTermine(Appointment termin)
        {
            for (int i = 0; i < terminZaPreporuku.Count; i++)
            {
                if (terminZaPreporuku[i].Vrijeme.Equals(termin.StartTime) && termin.Patient.Jmbg.Equals(PocetnaStranica.getInstance().Pacijent.Jmbg))
                {
                    terminZaPreporuku.RemoveAt(i);
                    i--;
                }
            }
        }
        private void preporukaZaDatum()
        {
            System.Console.WriteLine("Preporuka za datum");
            if (datumChecked.IsChecked == true)
            {
                for (int i= 0; i<termini.Count;i++)
                {
                    
                    izbaciZauzeteDoktore(termini[i]);
                    izbaciPacijentoveTermine(termini[i]);
                    
                }
            }
            
            int brojacPreporucenihTermina = 0;
            for (int i = 0; i < terminZaPreporuku.Count; i++)
            {
                if (terminZaPreporuku[i].IdLekara.Count == 0)
                {
                    continue;
                }

                brojacPreporucenihTermina++;
                Appointment termin = new Appointment();
                termin.StartTime = terminZaPreporuku[i].Vrijeme;
                termin.InitialTime = termin.StartTime;
                termin.Duration = 30;
                termin.Type = AppointmentType.examination;
                String idLekara =terminZaPreporuku[i].IdLekara[ i % terminZaPreporuku[i].IdLekara.Count];
                termin.Doctor = new DoctorFileRepository().FindById(idLekara);
                termin.Patient = PocetnaStranica.getInstance().Pacijent;
                termin.Room = new Room("1",RoomType.zaPreglede);
                termin.AppointmentID = DateTime.Now.ToString("yyMMddhhmmss");
                preporuceniTermini.Add(termin);
                if (brojacPreporucenihTermina == 5)
                    return;



            }
        }

        private void preporuka()
        {

            filtrirajTermine();
            
            if (lekarChecked.IsChecked == true)
            {
                preporukaZaDoktora();
            }
            else
            {
                preporukaZaDatum();
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
