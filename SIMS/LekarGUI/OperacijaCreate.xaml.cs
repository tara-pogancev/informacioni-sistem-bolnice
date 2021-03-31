﻿using Model;
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
    public partial class OperacijaCreate : Window
    {
        private List<Lekar> lekari;
        private List<Pacijent> pacijenti;
        private List<Prostorija> prostorije;
        private List<String> dostupniTermini;
        Termin termin = new Termin();

        public OperacijaCreate()
        {
            InitializeComponent();

            LekarStorage storageL = new LekarStorage();
            lekari = storageL.ReadList();

            PacijentStorage storageP = new PacijentStorage();
            pacijenti = storageP.ReadList();

            prostorije = new List<Prostorija>(ProstorijaStorage.Instance.ReadAll().Values);

            doktoriCombo.ItemsSource = lekari;
            pacijentiCombo.ItemsSource = pacijenti;
            prostorijeCombo.ItemsSource = prostorije;

            List<String> trajanjeVrednosti = new List<String>() { "30 minuta", "60 minuta", "90 minuta" };
            trajanjeLista.ItemsSource = trajanjeVrednosti;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Odraditi sve provere

            if (doktoriCombo.SelectedItem == null || datePicker1.SelectedDate == null || terminiLista.SelectedItem == null)
                MessageBox.Show("Molimo popunite sva polja!");
            else
            {
                Termin termin = new Termin();

                String vrijemeIDatum = datePicker1.Text + " " + terminiLista.Text;
                DateTime vremenskaOdrednica = DateTime.Parse(vrijemeIDatum);
                termin.PocetnoVreme = vremenskaOdrednica;

                if (trajanjeLista.SelectedIndex == 0)
                    termin.VremeTrajanja = new TimeSpan(0, 30, 0);
                else if (trajanjeLista.SelectedIndex == 1)
                    termin.VremeTrajanja = new TimeSpan(1, 0, 0);
                else
                    termin.VremeTrajanja = new TimeSpan(1, 30, 0);

                termin.Prostorija = prostorije[prostorijeCombo.SelectedIndex].Broj;
                termin.PacijentKey = pacijenti[pacijentiCombo.SelectedIndex].Jmbg;
                termin.LekarKey = lekari[doktoriCombo.SelectedIndex].Jmbg;
                termin.VrstaTermina = TipTermina.operacija;

                LekarUI.getInstance().dodajTermin(termin);
                this.Close();
            }
            
        }

        private void datePicker1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (doktoriCombo.SelectedItem != null)
            {
                Lekar lek = lekari[doktoriCombo.SelectedIndex];
                List<Termin> doktoroviTermini = new List<Termin>();
                dostupniTermini = new List<String>() { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00" };
                terminiLista.ItemsSource = dostupniTermini;
                /*
                foreach (Termin termin in new TerminStorage().ReadList())
                {
                    if (termin.LekarKey.Equals(lek.Jmbg) && datePicker1.SelectedDate.Value.Date.ToShortDateString().Equals(termin.Datum))
                    {
                        doktoroviTermini.Add(termin);
                    }
                }

                foreach (Termin termin in doktoroviTermini)
                {
                    dostupniTermini.Remove(termin.Vrijeme);
                }
                */
            }
        }

    }

}
