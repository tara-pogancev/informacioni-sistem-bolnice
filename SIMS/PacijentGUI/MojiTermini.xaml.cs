using SIMS.Repositories.SecretaryRepo;
using SIMS.Model;
using SIMS.Repositories.AppointmentRepo;
using SIMS.Repositories.DoctorRepo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;


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
            IAppointmentRepository appointmentRepository = new AppointmentFileRepository();
           
            termini = new ObservableCollection<Appointment>(appointmentRepository.GetAll());
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
            poc.frame.Content = izmjenaPregleda;
        }

        private bool provjeriValidnost()
        {
            int brojLogova = 0;
            List<AppointmentLog> terminLogs = new AppointmentLogFileRepository().ReadByPatient(pacijent);
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
            new PatientFileRepository().Update(pacijent);
            new AppointmentLogFileRepository().MakeLogExpired(pacijent);
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
            AppointmentFileRepository tr = new AppointmentFileRepository();
            Appointment termin = termini[terminiTabela.SelectedIndex];
            tr.Delete(termin.TerminKey);
            termini.RemoveAt(terminiTabela.SelectedIndex);
            AppointmentLog terminLog= new AppointmentLog(formirajKljucLog(termin), termin.TerminKey, pacijent.Jmbg, DateTime.Now, SurgeryType.Brisanje);
            new AppointmentLogFileRepository().Save(terminLog);
        }

        public String formirajKljucLog(Appointment termin)
        {
            return termin.TerminKey + PocetnaStranica.getInstance().Pacijent.Jmbg + DateTime.Now.ToString("hhmmss");
        }
        private void dobaviPodatkeOPacijenuILekaru()
        {
            foreach(Appointment termin in termini)
            {
                termin.Pacijent = new PatientFileRepository().FindById(termin.Pacijent.Jmbg);
                termin.Lekar = new DoctorFileRepository().FindById(termin.Lekar.Jmbg);
            }
        }
    }
}
