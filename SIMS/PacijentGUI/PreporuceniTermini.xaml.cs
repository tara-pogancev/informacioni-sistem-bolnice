using Model;
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
using System.Windows.Shapes;

namespace SIMS.PacijentGUI
{
    /// <summary>
    /// Interaction logic for PreporuceniTermini.xaml
    /// </summary>
    public partial class PreporuceniTermini : Page
    {
        private static ObservableCollection<Termin> termini;
        private Pacijent pacijent;
        public PreporuceniTermini()
        {
            
            
            InitializeComponent();
            this.DataContext = this;
            pacijent = PocetnaStranica.getInstance().Pacijent;
        }
        public static ObservableCollection<Termin> Termini { get => termini; set => termini = value; }
       
        public void dodajTermine(List<Termin> terminiPreporuka)
        {
            termini = new ObservableCollection<Termin>(terminiPreporuka);
        }

        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            ZakazivanjeTermina zakazivanje = ZakazivanjeTermina.getInstance();
            zakazivanje.Zakazivanje1.Children.Clear();
            zakazivanje.Zakazivanje1.Children.Add(new PreporukaTermina(pacijent));
            PocetnaStranica.getInstance().Tabovi.Content =zakazivanje;
            
        }

        private void Zakazi_Click(object sender, RoutedEventArgs e)
        {
            TerminStorage trm = new TerminStorage();
            trm.Create(termini[PreporuceniTerminiTabela.SelectedIndex]);
            termini.Remove(termini[PreporuceniTerminiTabela.SelectedIndex]);
        }
    }
}
