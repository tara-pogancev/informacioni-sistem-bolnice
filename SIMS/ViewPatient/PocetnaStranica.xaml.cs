﻿using SIMS.Repositories.SecretaryRepo;
using SIMS.Model;
using SIMS.Repositories.AppointmentRepo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SIMS.Service;
using SIMS.ViewPatient;

namespace SIMS.PacijentGUI
{
    /// <summary>
    /// Interaction logic for PocetnaStranica.xaml
    /// </summary>
    public partial class PocetnaStranica : Window
    {
        private Patient patient;
        private static PocetnaStranica instance=null;
        public static PocetnaStranica getInstance()
        {
            if (instance == null)
            {
                instance = new PocetnaStranica();
            }
            return instance;
        }
        
        private PocetnaStranica()
        {
            
            InitializeComponent();
            frame.Content = new PocetniEkran(patient);
            this.DataContext = this;
        }

        public void kreirajAnketu()
        {
           
            if (new HospitalSurveyService().ShowSurveyToPatient(patient))
            {
                Anketa.Visibility = Visibility.Visible;
            }
            
        }



        public void pokreniNit()
        {
            Thread provjeraPredstojecihTermina = new Thread(new ThreadStart(notifikacijaZaTermine));
            provjeraPredstojecihTermina.IsBackground = true;
            provjeraPredstojecihTermina.Start();
            
        }
        private void notifikacijaZaTermine()
        {
            while (true)
            {
                if (postojeTermini())
                {
                    this.Dispatcher.Invoke(() => {
                        ObavjestenjeOTerminu obavjestenjeOTerminu = new ObavjestenjeOTerminu();
                        obavjestenjeOTerminu.ShowDialog();
                    });
                }
                Thread.Sleep(1000 * 60 * 60);
            }
        }

        private bool postojeTermini()
        {
            List<Appointment> zakazaniTermini = new AppointmentFileRepository().GetPatientAppointments(patient);
            foreach(Appointment termin in zakazaniTermini)
            {
                if (DateTime.Now <= termin.StartTime && DateTime.Now.AddMinutes(60)>=termin.StartTime)
                {
                    return true;
                }

            }
            return false;

        }

       
        public Patient Pacijent { get => patient; set => patient = value; }

        private void Iskljucivanje_Click(object sender, RoutedEventArgs e)
        {
            
            new MainWindow().Show();
            instance = null;
            this.Close();

        }

        private void Zvonce_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate( new Obavjestenja());
        }

        private void ListViewMenu_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            int index = ListViewMenu.SelectedIndex;
            switch (index)
            {
                case 0:
                    frame.Navigate( new PocetniEkran(patient));
                    break;
                case 1:
                    ZakazivanjeTermina zakazivanje = ZakazivanjeTermina.getInstance();
                    zakazivanje.Pacijent = patient;
                    frame.Content = zakazivanje;
                    break;
                case 2:
                    frame.Navigate( new MojiTermini(patient));
                    break;
                case 3:
                    frame.Navigate( new TerapijaPacijentaView());
                    break;
                case 4:
                    frame.Navigate( new IzborLjekara());
                    break;
                case 5:
                    frame.Navigate(new IstorijaPregleda());
                    break;
                case 6:
                    frame.Navigate(new ReceptiIzvjestaji());
                    break;
                default:
                    break;
            }
        }

        private void Profil_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate( new KorisnickiProfil());
        }

        public void SetInstance()
        {
            instance = null;
        }

        private void Anketa_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate( new AnketaBolnicePage());
        }
    }
}
