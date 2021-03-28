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
        //private List<Pacijent> pacijenti;
        //private List<Prostorija> prostorije;
        private List<String> termini;
        private Termin t;

        public TerminUpdate(Termin termin)
        {
            InitializeComponent();

            LekarStorage storageL = new LekarStorage();
            lekari = storageL.Read();

            PacijentStorage storageP = new PacijentStorage();
            //pacijenti = storageP.Read();

            ProstorijaStorage storagePr = new ProstorijaStorage();
            //prostorije = storagePr.Read();

            doktoriCombo.ItemsSource = lekari;
            //pacijentiCombo.ItemSource = pacijenti;
            //prostorijeCombo.ItemSource = prostorije;

            this.t = termin;
            this.setValues();

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
                int durationMin = (duration.SelectedIndex + 1) * 15;
                int durationH = durationMin / 60;
                durationMin -= durationH * 60;

                TimeSpan terminSpan = new TimeSpan(durationH, durationMin, 0);

                t.Lekar = lekari[doktoriCombo.SelectedIndex];
                String vreme = datePicker1.Text + " " + terminiLista.Text;
                t.PocetnoVreme = DateTime.Parse(vreme);
                t.VremeTrajanja = terminSpan;
                t.VrstaTermina = TipTermina.operacija;

                this.Close();
            }
            
        }

        private void setValues()
        {
            termini = new List<String>() { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00" };
            terminiLista.ItemsSource = termini;

            datePicker1.DisplayDate = t.PocetnoVreme;
            datePicker1.Text = t.PocetnoVreme.ToString("dd/MM/yyyy");

            int minutes = (int)t.VremeTrajanja.TotalMinutes;
            //duration.SelectedIndex = (minutes / 30) - 1;
            duration.SelectedIndex = 6;

            int index = 0;
            foreach (Lekar lek in lekari)
            {
                if (lek.Jmbg.Equals(t.Lekar.Jmbg))
                {
                    break;
                }
                index++;
            }
            doktoriCombo.SelectedIndex = index;

            index = 0;
            foreach (String str in termini)
            {
                if (str.Equals(t.Vrijeme))
                {
                    break;
                }
                index++;
            }
            terminiLista.SelectedIndex = index;

        }


        private void datePicker1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (doktoriCombo.SelectedItem != null)
            {
                Lekar lek = lekari[doktoriCombo.SelectedIndex];
                List<Termin> doktoroviTermini = new List<Termin>();
                termini = new List<String>() { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00" };
                terminiLista.ItemsSource = termini;
                
                foreach (Termin termin in PacijentUI.getInstance().Termini)
                {
                    if (termin.Lekar.Jmbg.Equals(lek.Jmbg) && datePicker1.SelectedDate.Value.Date.ToShortDateString().Equals(termin.Datum))
                    {
                        doktoroviTermini.Add(termin);
                    }
                }

                foreach (Termin termin in doktoroviTermini)
                {
                    termini.Remove(termin.Vrijeme);
                }
            }
        }

    }

}
