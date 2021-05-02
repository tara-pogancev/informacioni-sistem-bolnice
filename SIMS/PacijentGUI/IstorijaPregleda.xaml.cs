using Model;
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
    /// <summary>
    /// Interaction logic for IstorijaPregleda.xaml
    /// </summary>
    public partial class IstorijaPregleda : Page
    {
        private List<Termin> termini;
        

        public IstorijaPregleda()
        {
            InitializeComponent();
            termini = new TerminStorage().ReadByPatient(PocetnaStranica.getInstance().Pacijent);
            prikazPoDatumu();
            dobaviLekare();

            this.DataContext = this;
        }
        public List<Termin> Termini { get => termini; set => termini = value; }
        

        private void dobaviLekare()//ucitavanje doktora iz fajla za svaki termin
        {
            LekarStorage lekarStorage = new LekarStorage();
            for (int i = 0; i < termini.Count; i++)
            {
                termini[i].Lekar = lekarStorage.Read(termini[i].Lekar.Jmbg);
            }
        }

        private void prikazPoDatumu() // izbacuje sve termine koji jos nisu prosli
        {
            for (int i = 0; i < termini.Count; i++)
            {
                if (termini[i].PocetnoVreme >= DateTime.Now)
                {
                    termini.RemoveAt(i);
                    i--;
                }
                
            }
        }

        

        private void Detalji_Click(object sender, RoutedEventArgs e)
        {
            AnamnezaStorage anamnezaStorage = new AnamnezaStorage();
            Termin termin = (Termin)terminiTabela.SelectedItem;
            
            Anamneza anamneza=anamnezaStorage.Read(termin.TerminKey);
            if (anamneza == null)
            {
                anamneza = new Anamneza();
                anamneza.Termin = termin;
            }
            PocetnaStranica.getInstance().Tabovi.Content = new DetaljiPregleda(anamneza);
        }

        private void Ocijeni_Click(object sender, RoutedEventArgs e)
        {
            
            Termin termin = (Termin)terminiTabela.SelectedItem;
            this.NavigationService.Navigate(new OcijeniPregled(termin));
            
        }
    }
}
