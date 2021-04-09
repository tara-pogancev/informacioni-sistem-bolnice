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
        private List<String> termini;
        List<String> imena = new List<String>();
        Termin termin;
        Boolean doktorSelektovan;
        
        public IzmjenaPregleda(Termin termin)
        {
            InitializeComponent();
            LekarStorage lekarStorage = new LekarStorage();
            TerminStorage terminStorage = new TerminStorage();
            lekari = lekarStorage.ReadList();
            doktorSelektovan = false;
            termini =  new List<String>() { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00" };
            this.termin = termin;
            filtrirajTermine();
            terminiLista.ItemsSource = termini;
            doktori.ItemsSource = lekari;
            CalendarDateRange cdr = new CalendarDateRange(DateTime.MinValue,termin.PocetnoVreme.AddDays(-3));
            CalendarDateRange cdr1 = new CalendarDateRange(termin.PocetnoVreme.AddDays(3), DateTime.MaxValue);
            datePicker1.BlackoutDates.Add(cdr);
            datePicker1.BlackoutDates.Add(cdr1);
            fillComboBoxes(termin);
        }

        private void filtrirajTermine()
        {
            
            foreach (Termin ter in new TerminStorage().ReadList())
            {
                if (termin.LekarKey.Equals(ter.LekarKey))
                {
                    if (termin.Datum.Equals(ter.Datum) && !termin.Vrijeme.Equals(ter.Vrijeme))
                        termini.Remove(ter.Vrijeme);
                }
            }
        }

        private void filtrirajTermine1()
        {
            Lekar lek = (Lekar)doktori.SelectedItem;
            termini = new List<string>() { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00" };
            List<Termin> sviTermini = new TerminStorage().ReadList();
            
          
            foreach (Termin ter in sviTermini)
            {
                if (termin.LekarKey.Equals(lek.Jmbg))
                {
                    if (datePicker1.SelectedDate.Value.Date.ToShortDateString().Equals(ter.Datum) && !termin.Vrijeme.Equals(ter.Vrijeme))
                        termini.Remove(ter.Vrijeme);
                }
            }
            terminiLista.ItemsSource = termini;
        }

        public void fillComboBoxes(Termin termin)
        {
            datePicker1.DisplayDate = termin.PocetnoVreme;
            datePicker1.Text = termin.PocetnoVreme.ToString("dd/MM/yyyy");
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
            index = 0;
            foreach (String str in termini)
            {

                if (str.Equals(termin.Vrijeme))
                {
                    break;
                }
                index++;
            }
            terminiLista.SelectedIndex = index;
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
                filtrirajTermine1();
            }
        }

        private void datePicker1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (doktorSelektovan)
            {
              
                filtrirajTermine1();
            }

        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
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
