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
    /// <summary>
    /// Interaction logic for AnketaBolnice.xaml
    /// </summary>
    public partial class AnketaBolnicePage : Page
    {
        public AnketaBolnicePage()
        {
            InitializeComponent();
        }

        private void Posalji_Click(object sender, RoutedEventArgs e)
        {
            AnketaBolnice anketaBolnice = new AnketaBolnice();
            anketaBolnice.IdVlasnika = PocetnaStranica.getInstance().Pacijent.Jmbg;
            anketaBolnice.IdAnkete = anketaBolnice.DatumKreiranjaAnkete + anketaBolnice.IdVlasnika;
            anketaBolnice.Komentar = KomentarPregleda.Text;
            anketaBolnice.OdgovoriNaPitanja.Add("pitanje1", Pitanje1.Value);
            anketaBolnice.OdgovoriNaPitanja.Add("pitanje2", Pitanje2.Value);
            anketaBolnice.OdgovoriNaPitanja.Add("pitanje3", Pitanje3.Value);
            anketaBolnice.OdgovoriNaPitanja.Add("pitanje4", Pitanje4.Value);
            anketaBolnice.OdgovoriNaPitanja.Add("pitanje5", Pitanje5.Value);
            anketaBolnice.TrenutniBrojPregleda = brojZavrsenihPRegleda();
            new AnketaBolniceStorage().Create(anketaBolnice);
            PocetnaStranica.getInstance().Anketa.Visibility = Visibility.Collapsed;
            PocetnaStranica.getInstance().Tabovi.Content = new PocetniEkran(PocetnaStranica.getInstance().Pacijent);


        }

        private int brojZavrsenihPRegleda()
        {
            List<Termin> zakazaniTermini = new TerminStorage().ReadByPatient(PocetnaStranica.getInstance().Pacijent);
            int brojacZavrsenihPregleda = 0;
            foreach (Termin termin in zakazaniTermini)
            {
                if (termin.IsPast)
                {
                    brojacZavrsenihPregleda++;
                }
            }
            return brojacZavrsenihPregleda;
        }
    }
}
