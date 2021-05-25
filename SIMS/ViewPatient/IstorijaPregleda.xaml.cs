using SIMS.Repositories.SecretaryRepo;
using SIMS.Model;
using SIMS.Repositories.AnamnesisRepository;
using SIMS.Repositories.AppointmentRepo;
using SIMS.Repositories.DoctorRepo;
using SIMS.Repositories.DoctorSurveyRepo;
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
using SIMS.Service;

namespace SIMS.PacijentGUI
{
    public class IstorijaPregledaView
    {
        private Appointment termin;
        private bool omogucenoOcjenjivanje;

        public IstorijaPregledaView() {
            termin = new Appointment();
            omogucenoOcjenjivanje = true;
        }
        public IstorijaPregledaView(Appointment termin)
        {
            this.termin = termin;
            omogucenoOcjenjivanje = new DoctorSurveyFileRepository().FindById(termin.AppointmentID) == null ? true : false;
        }

        public Appointment Termin { get => termin; set => termin = value; }
        public bool OmogucenoOcjenjivanje { get => omogucenoOcjenjivanje; set => omogucenoOcjenjivanje = value; }
    }
    public partial class IstorijaPregleda : Page
    {
        private List<IstorijaPregledaView> terminiZaPrikaz;
        private List<Appointment> pastAppointments;

        public List<IstorijaPregledaView> TerminiZaPrikaz { get => terminiZaPrikaz; set => terminiZaPrikaz = value; }
        

        public IstorijaPregleda()
        {
            InitializeComponent();
            pastAppointments = new AppointmentService().GetPastAppointments();
            dobaviLekare();
            terminiZaPrikaz = formirajTermine(pastAppointments);
            this.DataContext = this;
        }

        private List<IstorijaPregledaView> formirajTermine(List<Appointment> zakazaniTermini)
        {
            List<IstorijaPregledaView> terminiZaPrikaz = new List<IstorijaPregledaView>();
            foreach(Appointment termin in zakazaniTermini)
            {
                IstorijaPregledaView terminZaPrikaz = new IstorijaPregledaView(termin);
                terminiZaPrikaz.Add(terminZaPrikaz);
            }
            return terminiZaPrikaz;
        }

        private void dobaviLekare()//ucitavanje doktora iz fajla za svaki termin
        {
            IDoctorRepository lekarStorage = new DoctorFileRepository();
            for (int i = 0; i < pastAppointments.Count; i++)
            {
                pastAppointments[i].Doctor = lekarStorage.FindById(pastAppointments[i].Doctor.Jmbg);
            }
        }

        

        

        private void Detalji_Click(object sender, RoutedEventArgs e)
        {
            IAnamnesisRepository anamnezaStorage = new AnamnesisFileRepository();
            IstorijaPregledaView selektovaniTermin = (IstorijaPregledaView)terminiTabela.SelectedItem;
            
            Anamnesis anamneza=anamnezaStorage.FindById(selektovaniTermin.Termin.AppointmentID);
            if (anamneza == null)
            {
                anamneza = new Anamnesis();
                anamneza.AnamnesisAppointment = selektovaniTermin.Termin;
            }
            PocetnaStranica.getInstance().frame.Content = new DetaljiPregleda(anamneza);
        }

        private void Ocijeni_Click(object sender, RoutedEventArgs e)
        {
            IstorijaPregledaView selektovaniTermin = (IstorijaPregledaView)terminiTabela.SelectedItem;
            Appointment termin = selektovaniTermin.Termin;
            this.NavigationService.Navigate(new OcijeniPregled(termin));
            
        }
    }
}
