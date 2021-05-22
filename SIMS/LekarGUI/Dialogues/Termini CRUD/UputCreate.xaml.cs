﻿using Model;
using SIMS.DTO;
using SIMS.Model;
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

namespace SIMS.LekarGUI.Dialogues.Termini_CRUD
{
    /// <summary>
    /// Interaction logic for ActionsAfterReport.xaml
    /// </summary>
    public partial class UputCreate : Window
    {
        public ObservableCollection<Doctor> DoctorList { get; set; }
        public Specialization SellectedSpecialization { get; set; }
        public List<SpecializationDTO> AvailableSpecialization { get; set; }

        private Patient patient;

        public UputCreate(Patient patient)
        {
            this.patient = patient;

            InitializeComponent();

            DataContext = this;

            LabelPacijent.Content = "Pacijent: " + patient.ImePrezime;
            LabelDatum.Content = "Datum: " + DateTime.Today.ToString("dd.MM.yyyy.");

            DoctorList = new ObservableCollection<Doctor>();
            InitSpecialization();
            SpecijalizationComboBox.ItemsSource = AvailableSpecialization;
            DoctorComboBox.ItemsSource = DoctorList;

        }

        public void InitSpecialization()
        {
            AvailableSpecialization = new List<SpecializationDTO>();

            foreach (Doctor doctor in DoctorRepository.Instance.GetAll())
            {
                SpecializationDTO currentDoctorSpecialization = new SpecializationDTO(doctor.SpecijalizacijaLekara);

                if (!AvailableSpecialization.Contains(currentDoctorSpecialization));
                    AvailableSpecialization.Add(currentDoctorSpecialization);
            }
        }

        private void SpecializationChanged(object sender, SelectionChangedEventArgs e)
        {
            SellectedSpecialization = GetSellectedSpecialization();
            RefreshDoctorList(SellectedSpecialization);
        }

        private void RefreshDoctorList(Specialization specialization)
        {
            DoctorList = new ObservableCollection<Doctor>();
            foreach (Doctor doctor in DoctorRepository.Instance.GetAll())
            {
                if (doctor.SpecijalizacijaLekara.Equals(specialization))
                {
                    DoctorList.Add(doctor);
                }
            }

            DoctorComboBox.ItemsSource = DoctorList;
        }

        private Specialization GetSellectedSpecialization()
        {
            int idx = SpecijalizationComboBox.SelectedIndex;
            return AvailableSpecialization[idx].Specialization;
        }            

        private void AcceptButton(object sender, RoutedEventArgs e)
        {
            if (DoctorComboBox.SelectedItem != null)
            {
                CreateRefferal();
                this.Close();

                SendNotifications((Doctor)DoctorComboBox.SelectedItem);

                MessageBox.Show("Uput uspešno kreiran!");
            }
            else
            {
                MessageBox.Show("Molimo izaberite doktora!");
            }

        }

        private void CreateRefferal()
        {
            Doctor doctor = (Doctor)DoctorComboBox.SelectedItem;
            Referral refferal = new Referral(doctor, patient);
            ReferralRepository.Instance.Save(refferal);
        }

        private void SendNotifications(Doctor doctor)
        {
            String author = LekarUI.GetInstance().GetUser().ImePrezime;
            List<String> target = new List<string>();
            target.Add(this.patient.Jmbg);

            Notification notification = new Notification(author, DateTime.Now, ("Izdat uput za pregled kod lekara " + doctor.ImePrezime + ". Pogledajte ga na svom profilu."), target);
            NotificationRepository.Instance.Save(notification);
        }
    }
}
