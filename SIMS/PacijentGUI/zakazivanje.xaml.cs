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
        public zakazivanje(Pacijent p)
        {
            LekarStorage lk = new LekarStorage();
            lekari = new List<Lekar>();
            lekari = lk.ReadList();
            pacijent = p;
            dostupniTermini = new List<String>() { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00" };
            termin = new Termin();
            this.DataContext = this;
            InitializeComponent();
        }

        public List<Lekar> Lekari { get => lekari; set => lekari = value; }
        public List<string> DostupniTermini { get => dostupniTermini; set => dostupniTermini = value; }
        public Pacijent Pacijent { get => pacijent; set => pacijent = value; }
        public Termin Termin { get => termin; set => termin = value; }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            termin.LekarKey = lekari[ListaDoktora.SelectedIndex].Jmbg;
            String vrijemeIDatum = OdabirDatuma.Text + " " + terminiLista.Text;
            DateTime vremenskaOdrednica = DateTime.Parse(vrijemeIDatum);
            termin.PocetnoVreme = vremenskaOdrednica;
            termin.VremeTrajanja = 30;
            termin.PacijentKey = pacijent.Jmbg;
            termin.Prostorija = "1";
            MessageBox.Show("Termin je uspjesno zakazan");

            TerminStorage.Instance.Create(termin);
        }
    }
}