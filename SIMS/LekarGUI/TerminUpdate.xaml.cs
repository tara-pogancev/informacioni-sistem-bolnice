using Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SIMS
{
    /// <summary>
    /// Interaction logic for TerminCreate.xaml
    /// </summary>
    public partial class TerminUpdate : Window
    {
        private List<Lekar> lekari;
        private List<Pacijent> pacijenti;
        private List<Prostorija> prostorije;
        private List<String> termini;
        Termin termin;

        public TerminUpdate(Termin termin)
        {
            InitializeComponent();
            this.termin = termin;

            LekarStorage storageL = new LekarStorage();
            lekari = storageL.Read();

            PacijentStorage storageP = new PacijentStorage();
            pacijenti = storageP.ReadAll();

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
            //Kreiranje novog pregleda
            //TODO: Odraditi sve provere

            //TODO: dodati za izbor prostorije i pacijente
            if (doktoriCombo.SelectedItem == null || datePicker1.SelectedDate == null || terminiLista.SelectedItem == null)
                MessageBox.Show("Molimo popunite sva polja!");
            else
            {
                String vrijemeIDatum = datePicker1.Text + " " + terminiLista.Text;
                DateTime vremenskaOdrednica = DateTime.Parse(vrijemeIDatum);
                termin.PocetnoVreme = vremenskaOdrednica;

                if (trajanjeLista.SelectedIndex == 0)
                    termin.VremeTrajanja = new TimeSpan(0, 30, 0);
                else if (trajanjeLista.SelectedIndex == 1)
                    termin.VremeTrajanja = new TimeSpan(1, 0, 0);
                else
                    termin.VremeTrajanja = new TimeSpan(1, 30, 0);

                termin.Prostorija = prostorije[prostorijeCombo.SelectedIndex];
                termin.Pacijent = pacijenti[pacijentiCombo.SelectedIndex];
                termin.Lekar = lekari[doktoriCombo.SelectedIndex];

                LekarUI.getInstance().refresh();
                this.Close();
            }
        }

        private void setValues()
        {
            termini = new List<String>() { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00" };
            terminiLista.ItemsSource = termini;

            datePicker1.DisplayDate = termin.PocetnoVreme;
            datePicker1.Text = termin.PocetnoVreme.ToString("dd/MM/yyyy");

            int minutes = (int)termin.VremeTrajanja.TotalMinutes;

            if (minutes == 30) trajanjeLista.SelectedIndex = 0;
            else if (minutes == 60) trajanjeLista.SelectedIndex = 1;
            else trajanjeLista.SelectedIndex = 2;

            int index = 0;
            foreach (Lekar l in lekari)
            {
                if (l.Jmbg.Equals(termin.Lekar.Jmbg))
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
                if (p.Jmbg.Equals(termin.Pacijent.Jmbg))
                {
                    break;
                }
                index++;
            }
            pacijentiCombo.SelectedIndex = index;

            index = 0;
            foreach (Prostorija pr in prostorije)
            {
                if (pr.Broj.Equals(termin.Prostorija.Broj))
                {
                    break;
                }
                index++;
            }
            prostorijeCombo.SelectedIndex = index;

        }

        private void datePicker1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (doktoriCombo.SelectedIndex != -1)
            {
                Lekar lek = lekari[doktoriCombo.SelectedIndex];
                termini = new List<string>() { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00" };
                foreach (Termin ter in new TerminStorage().ReadAll())
                {
                    if (termin.Lekar.Jmbg.Equals(lek.Jmbg))
                    {
                        if (datePicker1.SelectedDate.Value.Date.ToShortDateString().Equals(ter.Datum) && !termin.Vrijeme.Equals(ter.Vrijeme))
                            termini.Remove(ter.Vrijeme);
                    }
                }
                terminiLista.ItemsSource = termini;
            }
        }

    }

}
