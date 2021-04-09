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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SIMS.PacijentGUI
{
    /// <summary>
    /// Interaction logic for MojiTermini.xaml
    /// </summary>
    public partial class MojiTermini : Page
    {
        Pacijent pacijent;
        private static ObservableCollection<Termin> termini;

        

        public MojiTermini(Pacijent p)
        {
            pacijent = p;
            termini = new ObservableCollection<Termin>(TerminStorage.Instance.ReadByPatient(p));
            this.DataContext = this;
            InitializeComponent();
        }

        public Pacijent Pacijent { get => pacijent; set => pacijent = value; }
        public static ObservableCollection<Termin> Termini { get => termini; set => termini = value; }

        

        private void Izmijeni_Click(object sender, RoutedEventArgs e)
        {
            IzmjenaPregleda izmjenaPregleda = new IzmjenaPregleda(termini[terminiTabela.SelectedIndex]);
            PocetnaStranica poc = PocetnaStranica.getInstance();
            poc.Tabovi.Content = izmjenaPregleda;
        }

        private void Obrisi_Click(object sender, RoutedEventArgs e)
        {
            TerminStorage tr = new TerminStorage();
            tr.Delete(termini[terminiTabela.SelectedIndex].TerminKey);
            termini.RemoveAt(terminiTabela.SelectedIndex);
            
            
        }
    }
}
