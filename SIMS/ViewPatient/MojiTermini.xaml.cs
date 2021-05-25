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
using SIMS.Service;

namespace SIMS.PacijentGUI
{
    /// <summary>
    /// Interaction logic for MojiTermini.xaml
    /// </summary>
    public partial class MojiTermini : Page
    {
        Patient patient;
        private static ObservableCollection<Appointment> termini;

        

        public MojiTermini(Patient p)
        {
            patient = p;
            IAppointmentRepository appointmentRepository = new AppointmentFileRepository();
           
            termini = new ObservableCollection<Appointment>(new AppointmentService().GetFutureAppointments(patient));
            dobaviPodatkeOPacijenuILekaru();
            this.DataContext = this;
            InitializeComponent();
            
        }

        public Patient Pacijent { get => patient; set => patient = value; }
        public static ObservableCollection<Appointment> Termini { get => termini; set => termini = value; }

        

        private void Izmijeni_Click(object sender, RoutedEventArgs e)
        {
            if (new AntiTrollService().CanChangeAppointment(patient) == false)
            {
                banujKorisnika();
            }
            else
            {
                IzmjenaPregleda izmjenaPregleda = new IzmjenaPregleda(termini[terminiTabela.SelectedIndex]);
                PocetnaStranica poc = PocetnaStranica.getInstance();
                poc.frame.Content = izmjenaPregleda;
            }
            
        }

       

        private void banujKorisnika()
        {
            
            ObavjestenjeOTerminu o = new ObavjestenjeOTerminu();
            o.Height = 250;
            o.TekstObavjestenja.Text = "Zbog učestalih izmjena zakazanih termina Vaš nalog je blokiran. Za dodatne informacije obratite se sekretaru bolnice!";
            o.ShowDialog();
            new AntiTrollService().BanUser(patient);
            new MainWindow().Show();
            PocetnaStranica.getInstance().Close();
            PocetnaStranica.getInstance(). SetInstance();
            
        }

        private void Obrisi_Click(object sender, RoutedEventArgs e)
        {
            if (new AntiTrollService().CanChangeAppointment(patient) == false)
            {
                banujKorisnika();
            }
            else
            {
                new AppointmentService().CancelAppointment(termini[terminiTabela.SelectedIndex]);
                termini.RemoveAt(terminiTabela.SelectedIndex);
            }
            
        }

       
        private void dobaviPodatkeOPacijenuILekaru()
        {
            foreach(Appointment termin in termini)
            {
                termin.Patient = new PatientFileRepository().FindById(termin.Patient.Jmbg);
                termin.Doctor = new DoctorFileRepository().FindById(termin.Doctor.Jmbg);
            }
        }
    }
}
