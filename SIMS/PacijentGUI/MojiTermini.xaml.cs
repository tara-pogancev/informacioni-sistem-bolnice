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
        Pacijent pacijent;
        private static ObservableCollection<Termin> termini;

        

        public MojiTermini(Pacijent p)
        {
            pacijent = p;
            termini = new ObservableCollection<Termin>(TerminStorage.Instance.ReadByPatient(p));
            dobaviPodatkeOPacijenuILekaru();
            this.DataContext = this;
            InitializeComponent();
        }

        public Pacijent Pacijent { get => pacijent; set => pacijent = value; }
        public static ObservableCollection<Termin> Termini { get => termini; set => termini = value; }

        

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
            List<TerminLog> terminLogs = new TerminLogStorage().ReadByPatient(pacijent);
            foreach(TerminLog terminLog in terminLogs)
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
            new PacijentStorage().Update(pacijent);
            new TerminLogStorage().logoviIstekli(pacijent);
            new MainWindow().Show();
            PocetnaStranica.getInstance().Close();
            //PocetnaStranica.getInstance(). SetInstance();
            
        }

        private void Obrisi_Click(object sender, RoutedEventArgs e)
        {
            if (provjeriValidnost() == false)
            {
                return;
            }
            TerminStorage tr = new TerminStorage();
            Termin termin = termini[terminiTabela.SelectedIndex];
            tr.Delete(termin.TerminKey);
            termini.RemoveAt(terminiTabela.SelectedIndex);
            TerminLog terminLog= new TerminLog(formirajKljucLog(termin), termin.TerminKey, pacijent.Jmbg, DateTime.Now, TipOperacije.Brisanje);
            new TerminLogStorage().Create(terminLog);
        }

        public String formirajKljucLog(Termin termin)
        {
            return termin.TerminKey + PocetnaStranica.getInstance().Pacijent.Jmbg + DateTime.Now.ToString("hhmmss");
        }
        private void dobaviPodatkeOPacijenuILekaru()
        {
            foreach(Termin termin in termini)
            {
                termin.Pacijent = new PacijentStorage().Read(termin.Pacijent.Jmbg);
                termin.Lekar = new LekarStorage().Read(termin.Lekar.Jmbg);
            }
        }
    }
}
