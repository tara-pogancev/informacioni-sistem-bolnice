﻿using Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace SIMS.SekretarGUI
{
    public partial class DodajOperacijuPage : Page
    {
        private List<Lekar> _doctors;
        private List<Pacijent> _patients;
        private List<Prostorija> _rooms;

        public DodajOperacijuPage()
        {
            InitializeComponent();

            _doctors = LekarStorage.Instance.ReadList();
            _patients = PacijentStorage.Instance.ReadList();

            _rooms = new List<Prostorija>(ProstorijaStorage.Instance.ReadAll().Values);

            doctorsComboBox.ItemsSource = _doctors;
            patientsComboBox.ItemsSource = _patients;
            roomsComboBox.ItemsSource = _rooms;

            List<string> duration = new List<string>() { "30 minuta", "60 minuta", "90 minuta" };
            durationComboBox.ItemsSource = duration;
        }

        private void AddExamination_Click(object sender, RoutedEventArgs e)
        {
            if (doctorsComboBox.SelectedItem == null || datePicker.SelectedDate == null || appointmentsComboBox.SelectedItem == null)
            {
                MessageBox.Show("Molimo popunite sva polja!");
                return;
            }

            Termin appointment = CreateAppointmentFromUserInput();
            if (IsAppointmentValid(appointment))
            {
                TerminStorage.Instance.Create(appointment);
                SekretarTerminiPage.GetInstance().RefreshView();

                NavigationService.Navigate(SekretarTerminiPage.GetInstance());
            }
        }

        private Termin CreateAppointmentFromUserInput()
        {
            Termin appointment = new Termin();
            string dateAndTime = datePicker.Text + " " + appointmentsComboBox.Text;
            DateTime appointmentDateAndTime = DateTime.Parse(dateAndTime);
            appointment.PocetnoVreme = appointmentDateAndTime;
            appointment.InicijalnoVrijeme = appointment.PocetnoVreme;

            if (durationComboBox.SelectedIndex == 0)
                appointment.VremeTrajanja = 30;
            else if (durationComboBox.SelectedIndex == 1)
                appointment.VremeTrajanja = 60;
            else
                appointment.VremeTrajanja = 90;

            appointment.Prostorija = _rooms[roomsComboBox.SelectedIndex];
            appointment.Pacijent = _patients[patientsComboBox.SelectedIndex];
            appointment.Lekar = _doctors[doctorsComboBox.SelectedIndex];
            appointment.VrstaTermina = TipTermina.operacija;

            return appointment;
        }

        private bool IsAppointmentValid(Termin appointment)
        {
            List<Termin> appointments = TerminStorage.Instance.ReadList();
            foreach (Termin a in appointments)
            {
                if (a.KrajnjeVreme > appointment.PocetnoVreme && a.PocetnoVreme < appointment.KrajnjeVreme)
                {
                    if (a.Lekar.Jmbg.Equals(appointment.Lekar.Jmbg))
                    {
                        MessageBox.Show("Lekar je zauzet u navedenom terminu.", "Zauzet termin");
                        return false;
                    }
                    else if (a.NazivProstorije.Equals(appointment.NazivProstorije))
                    {
                        MessageBox.Show("Prostorija je zauzeta u navedenom terminu.", "Zauzet termin");
                        return false;
                    }
                }
            }
            return true;
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (doctorsComboBox.SelectedItem != null)
            {
                List<string> freeAppointments = new List<string>() { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00" };
                appointmentsComboBox.ItemsSource = freeAppointments;
            }
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(SekretarTerminiPage.GetInstance());
        }
    }
}