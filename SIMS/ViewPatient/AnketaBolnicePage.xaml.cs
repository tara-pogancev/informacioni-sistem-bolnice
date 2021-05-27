﻿using SIMS.Repositories.SecretaryRepo;
using SIMS.Model;
using SIMS.Repositories.AppointmentRepo;
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
using SIMS.PacijentGUI.ViewModel;

namespace SIMS.PacijentGUI
{
    /// <summary>
    /// Interaction logic for AnketaBolnice.xaml
    /// </summary>
    public partial class AnketaBolnicePage : Page
    {
        public AnketaBolnicePage()
        {
            InitializeComponent();
            this.DataContext = new HospitalSurveyViewModel();
        }

       /* Kod prebacen u viewmodel
        * private void Posalji_Click(object sender, RoutedEventArgs e)
        {
            HospitalSurvey anketaBolnice = new HospitalSurvey();
            anketaBolnice.IdVlasnika = PocetnaStranica.getInstance().Pacijent.Jmbg;
            anketaBolnice.IdAnkete = anketaBolnice.DatumKreiranjaAnkete + anketaBolnice.IdVlasnika;
            anketaBolnice.Komentar = KomentarPregleda.Text;
            anketaBolnice.OdgovoriNaPitanja.Add("pitanje1", Pitanje1.Value);
            anketaBolnice.OdgovoriNaPitanja.Add("pitanje2", Pitanje2.Value);
            anketaBolnice.OdgovoriNaPitanja.Add("pitanje3", Pitanje3.Value);
            anketaBolnice.OdgovoriNaPitanja.Add("pitanje4", Pitanje4.Value);
            anketaBolnice.OdgovoriNaPitanja.Add("pitanje5", Pitanje5.Value);
            anketaBolnice.TrenutniBrojPregleda = brojZavrsenihPRegleda();
            new HospitalSurveyFileRepository().Save(anketaBolnice);
            PocetnaStranica.getInstance().Anketa.Visibility = Visibility.Collapsed;
            PocetnaStranica.getInstance().frame.Content = new PocetniEkran(PocetnaStranica.getInstance().Pacijent);


        }

        private int brojZavrsenihPRegleda()
        {
            List<Appointment> zakazaniTermini = new AppointmentFileRepository().GetPatientAppointments(PocetnaStranica.getInstance().Pacijent);
            int brojacZavrsenihPregleda = 0;
            foreach (Appointment termin in zakazaniTermini)
            {
                if (termin.IsPast)
                {
                    brojacZavrsenihPregleda++;
                }
            }
            return brojacZavrsenihPregleda;
        } */
    }
}