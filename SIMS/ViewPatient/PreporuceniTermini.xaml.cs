﻿using SIMS.Repositories.SecretaryRepo;
using SIMS.Repositories.AppointmentRepo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SIMS.Controller;
using SIMS.Model;

namespace SIMS.PacijentGUI
{
    /// <summary>
    /// Interaction logic for PreporuceniTermini.xaml
    /// </summary>
    public partial class PreporuceniTermini : Page
    {
        private static ObservableCollection<Appointment> termini;
        private Patient pacijent;
        public PreporuceniTermini()
        {
            
            
            InitializeComponent();
            this.DataContext = this;
            pacijent = PocetnaStranica.getInstance().Pacijent;
        }
        public static ObservableCollection<Appointment> Termini { get => termini; set => termini = value; }
       
        public void dodajTermine(List<Appointment> terminiPreporuka)
        {
            termini = new ObservableCollection<Appointment>(terminiPreporuka);
        }

        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            ZakazivanjeTermina zakazivanje = ZakazivanjeTermina.getInstance();
            zakazivanje.Zakazivanje1.Children.Clear();
            zakazivanje.Zakazivanje1.Children.Add(new PreporukaTermina(pacijent));
            PocetnaStranica.getInstance().frame.Content =zakazivanje;
            
        }

        private void Zakazi_Click(object sender, RoutedEventArgs e)
        {
            AppointmentController appointmentController = new AppointmentController();
            appointmentController.SaveAppointment(termini[PreporuceniTerminiTabela.SelectedIndex]);
            termini.Remove(termini[PreporuceniTerminiTabela.SelectedIndex]);
            ObavjestenjeOTerminu obavjestenje = new ObavjestenjeOTerminu();
            obavjestenje.TekstObavjestenja.Text = "Termin je uspješno zakazan";
            obavjestenje.ShowDialog();

        }
    }
}
