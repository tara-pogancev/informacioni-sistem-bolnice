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
    /// Interaction logic for IzmjenaPregleda.xaml
    /// </summary>
    
    public partial class IzmjenaPregleda : Page
    {

        private List<Lekar> lekari;
        private List<String> mogucíTermini;

        Termin termin;
        Boolean doktorSelektovan;
        
        
        public IzmjenaPregleda(Termin termin)
        {
            InitializeComponent();
            
            LekarStorage lekarStorage = new LekarStorage();
            TerminStorage terminStorage = new TerminStorage();
            lekari = lekarStorage.ReadList();
            doktorSelektovan = false;
            mogucíTermini =  new List<String>() { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00" };
            this.termin = termin;
            doktori.ItemsSource = lekari;
            CalendarDateRange cdr = new CalendarDateRange(DateTime.MinValue,termin.InicijalnoVrijeme.AddDays(-3));
            CalendarDateRange cdr1 = new CalendarDateRange(termin.InicijalnoVrijeme.AddDays(3), DateTime.MaxValue);
            datePicker1.BlackoutDates.Add(cdr);
            datePicker1.BlackoutDates.Add(cdr1);
            fillComboBoxes(termin);
            this.DataContext = this;
        }

       

        private void filtrirajTermine()
        {
            Lekar lekar = (Lekar)doktori.SelectedItem;
            mogucíTermini = new List<string>() { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00" };
            List<Termin> sviTermini = new TerminStorage().ReadList();
            
            foreach (Termin ter in sviTermini)
            {
                bool lekariJednaki = termin.LekarKey.Equals(lekar.Jmbg);
                bool pacijentiJednaki = termin.PacijentKey.Equals(ter.PacijentKey);
                if (ter.LekarKey.Equals(lekar.Jmbg) || termin.PacijentKey.Equals(ter.PacijentKey))
                {
                    if (datePicker1.SelectedDate.Value.Date.ToString("dd.MM.yyyy.").Equals(ter.Datum) && !termin.Vrijeme.Equals(ter.Vrijeme))
                        mogucíTermini.Remove(ter.Vrijeme);    
                }
            }
            terminiLista.ItemsSource = mogucíTermini;
        }

        private void fillDoctor()
        {
            int index = 0;
            foreach (Lekar lek in lekari)
            {
                if (lek.Jmbg.Equals(termin.LekarKey))
                {
                    break;
                }
                index++;
            }
            doktori.SelectedIndex = index;
        }

        public void fillTermin()
        {
            int index = 0;

            foreach (String str in mogucíTermini)
            {

                if (str.Equals(termin.Vrijeme))
                {
                    break;
                }
                index++;
            }
            terminiLista.SelectedIndex = index;
        }

        public void fillComboBoxes(Termin termin)
        {
            datePicker1.DisplayDate = termin.PocetnoVreme;
            datePicker1.Text = termin.PocetnoVreme.ToString("dd.MM.yyyy.");
            fillDoctor();
            fillTermin();
            doktorSelektovan = true;

        }

        private void doktori_DropDownOpened(object sender, EventArgs e)
        {
            doktori.SelectedItem = null;
        }

        private void doktori_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (doktori.SelectedItem != null)
            {
                
                filtrirajTermine();
                terminiLista.SelectedIndex = -1;
                
            }
        }

        private void datePicker1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (doktorSelektovan)
            {
                
                filtrirajTermine();
                terminiLista.SelectedIndex = -1;
            }

        }

        private bool validiraj()
        {
            if (doktori.SelectedItem == null || datePicker1.SelectedDate == null || terminiLista.SelectedItem == null)
            {
                MessageBox.Show("Molimo popunite sva polja!");
                return false;
            }
            if (datePicker1.SelectedDate.Value > termin.InicijalnoVrijeme.AddDays(2) || datePicker1.SelectedDate.Value < termin.InicijalnoVrijeme.AddDays(-3))
            {
                MessageBox.Show("Datum je samo moguce pomjeriti 2 dana od inicijalnog termina");
                return false ;
            }
            return true;
        }
        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            if (!validiraj())
            {
                return;
            }

            termin.LekarKey = lekari[doktori.SelectedIndex].Jmbg;
            String vrijemeIDatum = datePicker1.Text + " " + terminiLista.Text;
            DateTime vremenskaOdrednica = DateTime.Parse(vrijemeIDatum);
            termin.PocetnoVreme = vremenskaOdrednica;
            TerminStorage.Instance.Update(termin);
            MojiTermini mj = new MojiTermini(PocetnaStranica.getInstance().Pacijent);
            PocetnaStranica.getInstance().Tabovi.Content = mj;
            
        }

        private void Odbaci_Click(object sender, RoutedEventArgs e)
        {
            MojiTermini mj = new MojiTermini(PocetnaStranica.getInstance().Pacijent);
            PocetnaStranica.getInstance().Tabovi.Content = mj;

        }

        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            MojiTermini mj = new MojiTermini(PocetnaStranica.getInstance().Pacijent);
            PocetnaStranica.getInstance().Tabovi.Content = mj;
        }

        
    }
}
