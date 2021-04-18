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

    class TerminZaPreporuku
    {
        private List<String> idLekara;
        private DateTime vrijeme;
        public TerminZaPreporuku()
        {

        }
        public TerminZaPreporuku(DateTime vrijeme)
        {
            this.vrijeme = vrijeme;
            this.idLekara = new LekarStorage().getAllId();
        }

        public List<string> IdLekara { get => idLekara; set => idLekara = value; }
        public DateTime Vrijeme { get => vrijeme; set => vrijeme = value; }
    }

    public partial class PreporukaTermina : UserControl
    {
        Pacijent pacijent;
        List<Termin> termini;
        List<Termin> preporuceniTermini;
        List<TerminZaPreporuku> terminZaPreporuku;
        List<Lekar> lekari;
        public PreporukaTermina(Pacijent p)
        {
            InitializeComponent();
            pacijent = p;
            termini = new TerminStorage().ReadList();
            preporuceniTermini = new List<Termin>();
            //ListaDoktora.IsHitTestVisible = false;
            terminZaPreporuku = new List<TerminZaPreporuku>();
        
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

   
                TerminZaPreporuku terminZaPreporuku1 = new TerminZaPreporuku(datum1 + ts);
                TerminZaPreporuku terminZaPreporuku2 = new TerminZaPreporuku(datum1 + ts1);
                TerminZaPreporuku terminZaPreporuku3 = new TerminZaPreporuku(datum1 + ts2);
                terminZaPreporuku.Add(terminZaPreporuku1);
                terminZaPreporuku.Add(terminZaPreporuku2);
                terminZaPreporuku.Add(terminZaPreporuku3);
                datum1 = datum1.AddDays(1);
            }
        }

        private void izbaciZauzeteTermine(Termin termin)
        {
            for(int i= 0;i < terminZaPreporuku.Count;i++)
            {
                if (terminZaPreporuku[i].Vrijeme.Equals(termin.PocetnoVreme) && termin.LekarKey.Equals(lekari[ListaDoktora.SelectedIndex].Jmbg))
                {
                    terminZaPreporuku.RemoveAt(i);
                    i--;
                }
            }
        }
        private void preporukaZaDoktora()
        {
            if (lekarChecked.IsChecked == true)
            {
                foreach (Termin ter in termini)
                {
                    izbaciZauzeteTermine(ter);
                }
            }
            for (int i = 0; i < terminZaPreporuku.Count; i++)
            {
                Termin termin = new Termin();
                termin.PocetnoVreme = terminZaPreporuku[i].Vrijeme;
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

        private void izbaciZauzeteDoktore(Termin termin)
        {

            for (int i = 0; i < terminZaPreporuku.Count; i++)
            {
                
                if (terminZaPreporuku[i].Vrijeme.Equals(termin.PocetnoVreme))
                {
                    terminZaPreporuku[i].IdLekara.Remove(termin.LekarKey);
                    break;
                }
                
            }
        }
        private void izbaciPacijentoveTermine(Termin termin)
        {
            for (int i = 0; i < terminZaPreporuku.Count; i++)
            {
                if (terminZaPreporuku[i].Vrijeme.Equals(termin.PocetnoVreme) && termin.PacijentKey.Equals(PocetnaStranica.getInstance().Pacijent.Jmbg))
                {
                    terminZaPreporuku.RemoveAt(i);
                    i--;
                }
            }
        }
        private void preporukaZaDatum()
        {
            System.Console.WriteLine("Preporuka za datum");
            if (datumChecked.IsChecked == true)
            {
                for (int i= 0; i<termini.Count;i++)
                {
                    
                    izbaciZauzeteDoktore(termini[i]);
                    izbaciPacijentoveTermine(termini[i]);
                    
                }
            }
            
            int brojacPreporucenihTermina = 0;
            for (int i = 0; i < terminZaPreporuku.Count; i++)
            {
                if (terminZaPreporuku[i].IdLekara.Count == 0)
                {
                    continue;
                }

                brojacPreporucenihTermina++;
                Termin termin = new Termin();
                termin.PocetnoVreme = terminZaPreporuku[i].Vrijeme;
                termin.VremeTrajanja = 30;
                termin.VrstaTermina = TipTermina.pregled;
                termin.LekarKey = terminZaPreporuku[i].IdLekara[i%terminZaPreporuku[i].IdLekara.Count];
                termin.PacijentKey = PocetnaStranica.getInstance().Pacijent.Jmbg;
                termin.Prostorija = "1";
                termin.TerminKey = DateTime.Now.ToString("yyMMddhhmmss");
                preporuceniTermini.Add(termin);
                if (brojacPreporucenihTermina == 5)
                    return;



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
            PocetnaStranica.getInstance().Tabovi.Content = preporuceni;
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
