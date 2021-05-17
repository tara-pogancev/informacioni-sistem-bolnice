using Model;
using SIMS.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SIMS.PacijentGUI
{
    /// <summary>
    /// Interaction logic for MojiTermini.xaml
    /// </summary>
    public partial class MojiTermini : Page
    {
        Patient pacijent;
        private static ObservableCollection<Appointment> termini;

        

        public MojiTermini(Patient p)
        {
            pacijent = p;
            termini = new ObservableCollection<Appointment>(AppointmentRepository.Instance.ReadByPatient(p));
            dobaviPodatkeOPacijenuILekaru();
            this.DataContext = this;
            InitializeComponent();
        }

        public Patient Pacijent { get => pacijent; set => pacijent = value; }
        public static ObservableCollection<Appointment> Termini { get => termini; set => termini = value; }

        

        private void Izmijeni_Click(object sender, RoutedEventArgs e)
        {
            if (provjeriValidnost() == false)
            {
                return;
            }
            IzmjenaPregleda izmjenaPregleda = new IzmjenaPregleda(termini[terminiTabela.SelectedIndex]);
            PocetnaStranica poc = PocetnaStranica.getInstance();
            poc.Tabovi.Content = izmjenaPregleda;
        }

        private bool provjeriValidnost()
        {
            int brojLogova = 0;
            List<AppointmentLog> terminLogs = new AppointmentLogRepository().ReadByPatient(pacijent);
            foreach(AppointmentLog terminLog in terminLogs)
            {
                if (terminLog.DatumPromjene < DateTime.Now.AddDays(-10))
                {
                    continue;
                }
                brojLogova++;
            }
            if (brojLogova >= 5)
            {
                banujKorisnika();
                return false;
            }

            return true;
        }

        private void banujKorisnika()
        {
            //MessageBox.Show("Text");
            ObavjestenjeOTerminu o = new ObavjestenjeOTerminu();
            o.Height = 250;
            o.TekstObavjestenja.Text = "Zbog učestalih izmjena zakazanih termina Vaš nalog je blokiran. Za dodatne informacije obratite se sekretaru bolnice!";
            o.ShowDialog();
           
            pacijent.BanovanKorisnik = true;
            pacijent.Serijalizuj = true;
            new PatientRepository().Update(pacijent);
            new AppointmentLogRepository().logoviIstekli(pacijent);
            new MainWindow().Show();
            PocetnaStranica.getInstance().Close();
            PocetnaStranica.getInstance(). SetInstance();
            
        }

        private void Obrisi_Click(object sender, RoutedEventArgs e)
        {
            if (provjeriValidnost() == false)
            {
                return;
            }
            AppointmentRepository tr = new AppointmentRepository();
            Appointment termin = termini[terminiTabela.SelectedIndex];
            tr.Delete(termin.TerminKey);
            termini.RemoveAt(terminiTabela.SelectedIndex);
            AppointmentLog terminLog= new AppointmentLog(formirajKljucLog(termin), termin.TerminKey, pacijent.Jmbg, DateTime.Now, SurgeryType.Brisanje);
            new AppointmentLogRepository().Create(terminLog);
        }

        public String formirajKljucLog(Appointment termin)
        {
            return termin.TerminKey + PocetnaStranica.getInstance().Pacijent.Jmbg + DateTime.Now.ToString("hhmmss");
        }
        private void dobaviPodatkeOPacijenuILekaru()
        {
            foreach(Appointment termin in termini)
            {
                termin.Pacijent = new PatientRepository().Read(termin.Pacijent.Jmbg);
                termin.Lekar = new DoctorRepository().Read(termin.Lekar.Jmbg);
            }
        }
    }
}
