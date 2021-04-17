﻿using Model;
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
    /// Interaction logic for DodajOperacijuPage.xaml
    /// </summary>
    public partial class DodajOperacijuPage : Page
    {
        private List<Lekar> lekari;
        private List<Pacijent> pacijenti;
        private List<Prostorija> prostorije;
        private List<String> dostupniTermini;
        Termin termin = new Termin();

        public DodajOperacijuPage()
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
                    termin.VremeTrajanja = 30;
                else if (trajanjeLista.SelectedIndex == 1)
                    termin.VremeTrajanja = 60;
                else
                    termin.VremeTrajanja = 90;

                termin.Prostorija = prostorije[prostorijeCombo.SelectedIndex].Broj;
                termin.PacijentKey = pacijenti[pacijentiCombo.SelectedIndex].Jmbg;
                termin.LekarKey = lekari[doktoriCombo.SelectedIndex].Jmbg;
                termin.VrstaTermina = TipTermina.operacija;

                TerminStorage.Instance.Create(termin);
                SekretarTerminiPage.GetInstance().refresh();

                this.NavigationService.Navigate(SekretarTerminiPage.GetInstance());
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
            }
        }

        private void Button_Odustani(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(SekretarTerminiPage.GetInstance());
        }
    }
}
