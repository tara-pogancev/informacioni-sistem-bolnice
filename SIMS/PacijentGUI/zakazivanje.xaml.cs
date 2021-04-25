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

namespace SIMS.PacijentGUI
{
    /// <summary>
    /// Interaction logic for zakazivanje.xaml
    /// </summary>
    public partial class zakazivanje : UserControl
    {
        Pacijent pacijent;
        private List<Lekar> lekari;
        private List<String> dostupniTermini;
        private Termin termin;
        Boolean doktorSelektovan;
        public zakazivanje(Pacijent p)
        {
            InitializeComponent();
            LekarStorage lk = new LekarStorage();
            lekari = new List<Lekar>();
            lekari = lk.ReadList();
            pacijent = PocetnaStranica.getInstance().Pacijent;
            dostupniTermini = new List<String>() { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00" };
            termin = new Termin();
            this.DataContext = this;
            doktorSelektovan = false;
            
        }

        public List<Lekar> Lekari { get => lekari; set => lekari = value; }
        public List<string> DostupniTermini { get => dostupniTermini; set => dostupniTermini = value; }
        public Pacijent Pacijent { get => pacijent; set => pacijent = value; }
        public Termin Termin { get => termin; set => termin = value; }

        private bool validiraj()
        {
            if (ListaDoktora.SelectedItem == null || OdabirDatuma.SelectedDate == null || terminiLista.SelectedItem == null)
            {
                MessageBox.Show("Molimo popunite sva polja!");
                return false;
            }
           
            return true;
        }
        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            if (!validiraj())
            {
                return;
            }
            termin.Lekar = lekari[ListaDoktora.SelectedIndex];
            String vrijemeIDatum = OdabirDatuma.Text + " " + terminiLista.Text;
            DateTime vremenskaOdrednica = DateTime.Parse(vrijemeIDatum);
            termin.PocetnoVreme = vremenskaOdrednica;
            termin.InicijalnoVrijeme = vremenskaOdrednica;
            termin.VremeTrajanja = 30;
            termin.Pacijent = pacijent;
            termin.Prostorija = new Prostorija("1",true,TipProstorije.zaPreglede);
            MessageBox.Show("Termin je uspjesno zakazan");
            termin.Lekar.Serijalizuj = false;
            termin.Pacijent.Serijalizuj = false;
            termin.Prostorija.Serijalizuj = false;


            TerminStorage.Instance.Create(termin);
        }

        private void ListaDoktora_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            doktorSelektovan = true;

        }

        private void OdabirDatuma_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (doktorSelektovan)
            {
                Lekar lek = lekari[ListaDoktora.SelectedIndex];
                List<Termin> nedostupniTermini = new List<Termin>();
                dostupniTermini = new List<String>() { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00" };
                terminiLista.ItemsSource = dostupniTermini;
                List<Termin> sviTermini = new TerminStorage().ReadList();
          
                foreach (Termin termin in sviTermini)
                {
                    if ((termin.Lekar.Jmbg.Equals(lek.Jmbg) && OdabirDatuma.SelectedDate.Value.Date.ToString("dd.mm.yyyy").Equals(termin.PocetnoVreme.ToString("dd.mm.yyyy")))
                    || (termin.Pacijent.Jmbg.Equals(pacijent.Jmbg) && OdabirDatuma.SelectedDate.Value.Date.ToString("dd.mm.yyyy").Equals(termin.PocetnoVreme.ToString("dd.mm.yyyy"))))
                    {
                        nedostupniTermini.Add(termin);
                    }
                }

                foreach (Termin termin in nedostupniTermini)
                {
                    dostupniTermini.Remove(termin.Vrijeme);
                }
                
            }
        }
    }
}
