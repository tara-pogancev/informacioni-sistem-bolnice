using Model;
using SIMS.Model;
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
            omogucenoOcjenjivanje = new DoctorSurveyRepository().Read(termin.TerminKey) == null ? true : false;
        }

        public Appointment Termin { get => termin; set => termin = value; }
        public bool OmogucenoOcjenjivanje { get => omogucenoOcjenjivanje; set => omogucenoOcjenjivanje = value; }
    }
    public partial class IstorijaPregleda : Page
    {
        private List<IstorijaPregledaView> terminiZaPrikaz;
        private List<Appointment> zakazaniTermini;

        public List<IstorijaPregledaView> TerminiZaPrikaz { get => terminiZaPrikaz; set => terminiZaPrikaz = value; }
        

        public IstorijaPregleda()
        {
            InitializeComponent();
            zakazaniTermini = new AppointmentRepository().ReadByPatient(PocetnaStranica.getInstance().Pacijent);
            prikazPoDatumu();
            dobaviLekare();
            terminiZaPrikaz = formirajTermine(zakazaniTermini);
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
            DoctorRepository lekarStorage = new DoctorRepository();
            for (int i = 0; i < zakazaniTermini.Count; i++)
            {
                zakazaniTermini[i].Lekar = lekarStorage.Read(zakazaniTermini[i].Lekar.Jmbg);
            }
        }

        private void prikazPoDatumu() // izbacuje sve termine koji jos nisu prosli
        {
            for (int i = 0; i < zakazaniTermini.Count; i++)
            {
                if (zakazaniTermini[i].PocetnoVreme >= DateTime.Now)
                {
                    zakazaniTermini.RemoveAt(i);
                    i--;
                }
                
            }
        }

        

        private void Detalji_Click(object sender, RoutedEventArgs e)
        {
            AnamnesisRepository anamnezaStorage = new AnamnesisRepository();
            IstorijaPregledaView selektovaniTermin = (IstorijaPregledaView)terminiTabela.SelectedItem;
            
            Anamnesis anamneza=anamnezaStorage.Read(selektovaniTermin.Termin.TerminKey);
            if (anamneza == null)
            {
                anamneza = new Anamnesis();
                anamneza.Termin = selektovaniTermin.Termin;
            }
            PocetnaStranica.getInstance().Tabovi.Content = new DetaljiPregleda(anamneza);
        }

        private void Ocijeni_Click(object sender, RoutedEventArgs e)
        {
            IstorijaPregledaView selektovaniTermin = (IstorijaPregledaView)terminiTabela.SelectedItem;
            Appointment termin = selektovaniTermin.Termin;
            this.NavigationService.Navigate(new OcijeniPregled(termin));
            
        }
    }
}
