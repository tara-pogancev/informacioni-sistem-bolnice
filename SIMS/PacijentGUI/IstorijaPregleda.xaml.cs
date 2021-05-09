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
        private Termin termin;
        private bool omogucenoOcjenjivanje;

        public IstorijaPregledaView() {
            termin = new Termin();
            omogucenoOcjenjivanje = true;
        }
        public IstorijaPregledaView(Termin termin)
        {
            this.termin = termin;
            omogucenoOcjenjivanje = new AnketaLekaraStorage().Read(termin.TerminKey) == null ? true : false;
        }

        public Termin Termin { get => termin; set => termin = value; }
        public bool OmogucenoOcjenjivanje { get => omogucenoOcjenjivanje; set => omogucenoOcjenjivanje = value; }
    }
    public partial class IstorijaPregleda : Page
    {
        private List<IstorijaPregledaView> terminiZaPrikaz;
        private List<Termin> zakazaniTermini;

        public List<IstorijaPregledaView> TerminiZaPrikaz { get => terminiZaPrikaz; set => terminiZaPrikaz = value; }
        

        public IstorijaPregleda()
        {
            InitializeComponent();
            zakazaniTermini = new TerminStorage().ReadByPatient(PocetnaStranica.getInstance().Pacijent);
            prikazPoDatumu();
            dobaviLekare();
            terminiZaPrikaz = formirajTermine(zakazaniTermini);
            this.DataContext = this;
        }

        private List<IstorijaPregledaView> formirajTermine(List<Termin> zakazaniTermini)
        {
            List<IstorijaPregledaView> terminiZaPrikaz = new List<IstorijaPregledaView>();
            foreach(Termin termin in zakazaniTermini)
            {
                IstorijaPregledaView terminZaPrikaz = new IstorijaPregledaView(termin);
                terminiZaPrikaz.Add(terminZaPrikaz);
            }
            return terminiZaPrikaz;
        }

        private void dobaviLekare()//ucitavanje doktora iz fajla za svaki termin
        {
            LekarStorage lekarStorage = new LekarStorage();
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
            AnamnezaStorage anamnezaStorage = new AnamnezaStorage();
            IstorijaPregledaView selektovaniTermin = (IstorijaPregledaView)terminiTabela.SelectedItem;
            
            Anamneza anamneza=anamnezaStorage.Read(selektovaniTermin.Termin.TerminKey);
            if (anamneza == null)
            {
                anamneza = new Anamneza();
                anamneza.Termin = selektovaniTermin.Termin;
            }
            PocetnaStranica.getInstance().Tabovi.Content = new DetaljiPregleda(anamneza);
        }

        private void Ocijeni_Click(object sender, RoutedEventArgs e)
        {
            IstorijaPregledaView selektovaniTermin = (IstorijaPregledaView)terminiTabela.SelectedItem;
            Termin termin = selektovaniTermin.Termin;
            this.NavigationService.Navigate(new OcijeniPregled(termin));
            
        }
    }
}
