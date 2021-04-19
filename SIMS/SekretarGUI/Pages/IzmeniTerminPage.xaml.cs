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

namespace SIMS.SekretarGUI
{
    /// <summary>
    /// Interaction logic for IzmeniTerminPage.xaml
    /// </summary>
    public partial class IzmeniTerminPage : Page
    {
        private List<Lekar> lekari;
        private List<Pacijent> pacijenti;
        private List<Prostorija> prostorije;
        private List<String> termini;
        Termin termin;

        public IzmeniTerminPage(Termin t)
        {
            InitializeComponent();
            this.termin = t;

            LekarStorage storageL = new LekarStorage();
            lekari = storageL.ReadList();

            PacijentStorage storageP = new PacijentStorage();
            pacijenti = storageP.ReadList();

            prostorije = new List<Prostorija>(ProstorijaStorage.Instance.ReadAll().Values);

            doktoriCombo.ItemsSource = lekari;
            pacijentiCombo.ItemsSource = pacijenti;
            prostorijeCombo.ItemsSource = prostorije;

            this.setValues();

            List<String> trajanjeVrednosti = new List<String>() { "30 minuta", "60 minuta", "90 minuta" };
            trajanjeLista.ItemsSource = trajanjeVrednosti;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Izmena pregleda
            //TODO: Odraditi sve provere

            if (doktoriCombo.SelectedItem == null || datePicker1.SelectedDate == null || terminiLista.SelectedItem == null)
                MessageBox.Show("Molimo popunite sva polja!");
            else
            {
                String vrijemeIDatum = datePicker1.Text + " " + terminiLista.Text;
                DateTime vremenskaOdrednica = DateTime.Parse(vrijemeIDatum);
                termin.PocetnoVreme = vremenskaOdrednica;

                if (trajanjeLista.SelectedIndex == 0)
                    termin.VremeTrajanja = 30;
                else if (trajanjeLista.SelectedIndex == 1)
                    termin.VremeTrajanja = 60;
                else
                    termin.VremeTrajanja = 90;

                termin.Prostorija = prostorije[prostorijeCombo.SelectedIndex].Broj;
                termin.PacijentKey = pacijenti[pacijentiCombo.SelectedIndex].Jmbg;
                termin.LekarKey = lekari[doktoriCombo.SelectedIndex].Jmbg;


                List<Termin> listaTermmina = TerminStorage.Instance.ReadList();
                foreach (Termin t in listaTermmina)
                {
                    if (t.PocetnoVreme.Day == termin.PocetnoVreme.Day && t.PocetnoVreme.Month == termin.PocetnoVreme.Month && t.PocetnoVreme.Year == termin.PocetnoVreme.Year && t.PocetnoVreme.TimeOfDay.Add(new TimeSpan(0, t.VremeTrajanja, 0)) > termin.PocetnoVreme.TimeOfDay && t.PocetnoVreme.TimeOfDay < termin.PocetnoVreme.TimeOfDay.Add(new TimeSpan(0, termin.VremeTrajanja, 0)) && !t.TerminKey.Equals(termin.TerminKey))
                    {
                        if (t.LekarKey.Equals(termin.LekarKey))
                        {
                            MessageBox.Show("Lekar je zauzet u navedenom terminu.", "Zauzet termin");
                            return;
                        }
                        else if (t.NazivProstorije.Equals(termin.NazivProstorije))
                        {
                            MessageBox.Show("Prostorija je zauzeta u navedenom terminu.", "Zauzet termin");
                            return;
                        }

                    }
                }

                TerminStorage.Instance.Update(termin);

                SekretarTerminiPage.GetInstance().refresh();

                this.NavigationService.Navigate(SekretarTerminiPage.GetInstance());
            }
        }

        private void setValues()
        {
            termini = new List<String>() { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00" };
            terminiLista.ItemsSource = termini;

            datePicker1.DisplayDate = termin.PocetnoVreme;
            datePicker1.Text = termin.PocetnoVreme.ToString("dd.MM.yyyy.");


            if (termin.VremeTrajanja == 30)
                trajanjeLista.SelectedIndex = 0;
            else if (termin.VremeTrajanja == 60)
                trajanjeLista.SelectedIndex = 1;
            else
                trajanjeLista.SelectedIndex = 2;

            int index = 0;
            foreach (Lekar l in lekari)
            {
                if (l.Jmbg.Equals(termin.LekarKey))
                {
                    break;
                }
                index++;
            }
            doktoriCombo.SelectedIndex = index;

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

            index = 0;
            foreach (Pacijent p in pacijenti)
            {
                if (p.Jmbg.Equals(termin.PacijentKey))
                {
                    break;
                }
                index++;
            }
            pacijentiCombo.SelectedIndex = index;

            index = 0;
            foreach (Prostorija pr in prostorije)
            {
                if (pr.Broj.Equals(termin.Prostorija))
                {
                    break;
                }
                index++;
            }
            prostorijeCombo.SelectedIndex = index;

        }

        private void datePicker1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            /*
            if (doktoriCombo.SelectedIndex != -1)
            {
                Lekar lek = lekari[doktoriCombo.SelectedIndex];
                termini = new List<string>() { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00" };
                foreach (Termin ter in new TerminStorage().ReadList())
                {
                    if (termin.LekarKey.Equals(lek.Jmbg))
                    {
                        if (datePicker1.SelectedDate.Value.Date.ToShortDateString().Equals(ter.Datum) && !termin.Vrijeme.Equals(ter.Vrijeme))
                            termini.Remove(ter.Vrijeme);
                    }
                }
            
                terminiLista.ItemsSource = termini;
            
            }
            */
        }

        private void Button_Odustani(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(SekretarTerminiPage.GetInstance());
        }
    }
}
