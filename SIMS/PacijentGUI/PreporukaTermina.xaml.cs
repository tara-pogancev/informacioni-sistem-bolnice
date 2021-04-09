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
    /// Interaction logic for PreporukaTermina.xaml
    /// </summary>
    /// 

    public partial class PreporukaTermina : UserControl
    {
        Pacijent pacijent;
        List<Termin> termini;
        List<Termin> preporuceniTermini;
        List<DateTime> terminiZaPreporuku;
        List<Lekar> lekari;
        public PreporukaTermina(Pacijent p)
        {
            InitializeComponent();
            pacijent = p;
            termini = new TerminStorage().ReadList();
            preporuceniTermini = new List<Termin>();
            //ListaDoktora.IsHitTestVisible = false;
            terminiZaPreporuku = new List<DateTime>();
            lekari = new LekarStorage().ReadList();
            this.DataContext = this;
        }

        private void filtrirajTermine()
        {
            DateTime datum1 = (DateTime)PocetniDatum.SelectedDate;
            DateTime datum2 = (DateTime)KrajnjiDatum.SelectedDate;
            TimeSpan ts = new TimeSpan(8, 0, 0);
            TimeSpan ts1 = new TimeSpan(9, 0, 0);
            TimeSpan ts2 = new TimeSpan(10, 0, 0);

            while (datum1 <= datum2)
            {
                terminiZaPreporuku.Add(datum1 + ts);
                terminiZaPreporuku.Add(datum1 + ts1);
                terminiZaPreporuku.Add(datum1 + ts2);
                datum1 = datum1.AddDays(1);
            }
        }

        private void preporukaZaDoktora()
        {
            if (lekarChecked.IsChecked == true)
            {
                foreach (Termin ter in termini)
                {
                    if (terminiZaPreporuku.Contains(ter.PocetnoVreme) || !ter.LekarKey.Equals(lekari[ListaDoktora.SelectedIndex].Jmbg))
                    {
                        terminiZaPreporuku.Remove(ter.PocetnoVreme);
                    }
                }
            }
            for (int i = 0; i < terminiZaPreporuku.Count; i++)
            {
                Termin termin = new Termin();
                termin.PocetnoVreme = terminiZaPreporuku[i];
                termin.VremeTrajanja = 30;
                termin.VrstaTermina = TipTermina.pregled;
                termin.LekarKey = lekari[ListaDoktora.SelectedIndex].Jmbg;
                termin.PacijentKey = PocetnaStranica.getInstance().Pacijent.Jmbg;
                termin.Prostorija = "1";
                termin.TerminKey = DateTime.Now.ToString("yyMMddhhmmss");
                preporuceniTermini.Add(termin);
                if (i == 4)
                {
                    break;
                }
            }
        }

        private void preporukaZaDatum()
        {
            if (datumChecked.IsChecked == true)
            {
                foreach (Termin ter in termini)
                {
                    if (terminiZaPreporuku.Contains(ter.PocetnoVreme))
                    {
                        terminiZaPreporuku.Remove(ter.PocetnoVreme);
                    }
                }
            }
            Random rnd = new Random();
            for (int i = 0; i < terminiZaPreporuku.Count; i++)
            {
                Termin termin = new Termin();
                termin.PocetnoVreme = terminiZaPreporuku[i];
                termin.VremeTrajanja = 30;
                termin.VrstaTermina = TipTermina.pregled;
                termin.LekarKey = lekari[rnd.Next(lekari.Count)].Jmbg;
                termin.PacijentKey = PocetnaStranica.getInstance().Pacijent.Jmbg;
                termin.Prostorija = "1";
                termin.TerminKey = DateTime.Now.ToString("yyMMddhhmmss");
                preporuceniTermini.Add(termin);
                if (i == 4)
                {
                    break;
                }
            }
        }

        private void preporuka()
        {

            filtrirajTermine();
            
            if (lekarChecked.IsChecked == true)
            {
                preporukaZaDoktora();
            }
            else
            {
                preporukaZaDatum();
            }

            
        }
        public Pacijent Pacijent { get => pacijent; set => pacijent = value; }
        public List<Lekar> Lekari { get => lekari; set => lekari = value; }

        private void Trazi_Click(object sender, RoutedEventArgs e)
        {
            preporuka();
            PreporuceniTermini preporuceni = new PreporuceniTermini();
            preporuceni.dodajTermine(preporuceniTermini);
            preporuceni.Show();
        }

        private void PocetniDatum_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime oznaceniDatum = (DateTime)PocetniDatum.SelectedDate;
            CalendarDateRange cdr = new CalendarDateRange(DateTime.MinValue,oznaceniDatum );
            CalendarDateRange cdr1 = new CalendarDateRange(oznaceniDatum.AddDays(7), DateTime.MaxValue);
            KrajnjiDatum.BlackoutDates.Add(cdr);
            KrajnjiDatum.BlackoutDates.Add(cdr1);
        }
    }
}
