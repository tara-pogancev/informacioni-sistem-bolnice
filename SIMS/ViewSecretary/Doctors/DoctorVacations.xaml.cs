using SIMS.Controller;
using SIMS.DTO;
using SIMS.Model;
using SIMS.ViewSecretary.Home;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace SIMS.ViewSecretary.Doctors
{
    public partial class DoctorVacations : Page
    {
        private List<DoctorDTO> _doctors;
        private DoctorController doctorController = new DoctorController();

        public DoctorVacations()
        {
            InitializeComponent();

            _doctors = doctorController.GetAllDoctorsDTO();
            doctorsComboBox.ItemsSource = _doctors;
        }

        private void AddVacation_Click(object sender, RoutedEventArgs e)
        {
            if (doctorsComboBox.SelectedItem == null || vacationDaysTextBox.Text.Trim().Equals("") || startDatePicker.SelectedDate == null || endDatePicker.SelectedDate == null)
            {
                if (doctorsComboBox.SelectedItem != null && !vacationDaysTextBox.Text.Trim().Equals(""))
                {
                    Doctor doctor = _doctors[doctorsComboBox.SelectedIndex];
                    int.TryParse(vacationDaysTextBox.Text, out int vacationDays);
                    doctor.VacationDays = vacationDays;
                    doctorController.UpdateDoctor(doctor);
                    CustomMessageBox.Show(TranslationSource.Instance["VacationDaysUpdatedMessage"]);

                    NavigationService.Navigate(ViewHome.GetInstance());
                }
                else
                {
                    CustomMessageBox.Show(TranslationSource.Instance["FillFieldsMessage"]);
                    return;
                }
            }
            else
            {
                Doctor doctor = _doctors[doctorsComboBox.SelectedIndex];
                VacationPeriod vacationPeriod = CreateVacationFromUserInput();
                int.TryParse(vacationDaysTextBox.Text, out int vacationDays);

                doctor.VacationDays = vacationDays;
                doctor.VacationPeriods.Add(vacationPeriod);
                doctor.VacationDays -= (vacationPeriod.EndTime - vacationPeriod.StartTime).Days;
                doctorController.UpdateDoctor(doctor);

                CustomMessageBox.Show(TranslationSource.Instance["VacationCreatedMessage"]);

                NavigationService.Navigate(ViewHome.GetInstance());
            }
        }

        private VacationPeriod CreateVacationFromUserInput()
        {
            DateTime startDateAndTime = DateTime.Parse(startDatePicker.Text);
            DateTime endDateAndTime = DateTime.Parse(endDatePicker.Text);

            VacationPeriod vacationPeriod = new VacationPeriod(startDateAndTime, endDateAndTime);

            return vacationPeriod;
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(ViewHome.GetInstance());
        }

        private void DoctorChanged(object sender, SelectionChangedEventArgs e)
        {
            Doctor doctor = _doctors[doctorsComboBox.SelectedIndex];
            vacationDaysTextBox.Text = doctor.VacationDays.ToString();
        }
    }
}
