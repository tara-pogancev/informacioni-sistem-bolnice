using SIMS.Commands;
using SIMS.Controller;
using SIMS.DTO;
using SIMS.Model;
using SIMS.ViewSecretary.Home;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace SIMS.ViewSecretary.ViewModel
{
    public class DoctorVacationsViewModel : ViewModelSecretary
    {
        public List<DoctorDTO> Doctors { get; set; }
        private Doctor doctorToUpdate;
        public Doctor DoctorToUpdate
        {
            get
            {
                return doctorToUpdate;
            }
            set
            {
                doctorToUpdate = value;
                VacationDays = doctorToUpdate.VacationDays.ToString();
                //OnPropertyRaised("VacationDays");
                

            }
        }
        public int DoctorSelectedIndex { get; set; }
        private string vacationDays;
        public string VacationDays
        {
            get
            {
                return vacationDays;
            }
            set
            {
                vacationDays = value;
                OnPropertyChanged("VacationDays");
            }
        }
        public DateTime SelectedDateStart { get; set; }
        public string SelectedDateTextStart { get; set; }
        public DateTime SelectedDateEnd { get; set; }
        public string SelectedDateTextEnd { get; set; }

        private DoctorController doctorController = new DoctorController();
        public RelayCommand AddVacationCommand { get; set; }
        public RelayCommand QuitCommand { get; set; }
        public RelayCommand DoctorChangedCommand { get; set; }

        public DoctorVacationsViewModel()
        {
            Doctors = doctorController.GetAllDoctorsDTO();
            AddVacationCommand = new RelayCommand(Execute_AddVacationCommand, CanExecute_AddVacationCommand);
            QuitCommand = new RelayCommand(Execute_QuitCommand, CanExecute_QuitCommand);
            DoctorChangedCommand = new RelayCommand(Execute_DoctorChangedCommand, CanExecute_DoctorChangedCommand);
            SelectedDateStart = DateTime.Now;
            SelectedDateTextStart = SelectedDateStart.ToString();
            SelectedDateEnd = DateTime.Now;
            SelectedDateTextEnd = SelectedDateEnd.ToString();
        }

        private bool IsValid()
        {
            string strRegex = @"[0-9]+$";

            Regex re = new Regex(strRegex);
            if (String.IsNullOrEmpty(VacationDays) || VacationDays.Trim().Equals("") || !re.IsMatch(VacationDays))
            {
                CustomMessageBox.Show(TranslationSource.Instance["VacationNotNumberMessage"]);
                return false;
            }
            if (SelectedDateEnd.Date <= SelectedDateStart.Date)
            {
                CustomMessageBox.Show(TranslationSource.Instance["InvalidDatesMessage"]);
                return false;
            }
            return true;
        }

        private void Execute_AddVacationCommand(object obj)
        {
            if (DoctorToUpdate == null || VacationDays.Trim().Equals("") || SelectedDateStart == null || SelectedDateEnd == null)
            {
                if (DoctorToUpdate != null && !VacationDays.Trim().Equals(""))
                {
                    if (!IsValid())
                    {
                        return;
                    }
                    Doctor doctor = Doctors[DoctorSelectedIndex];
                    int.TryParse(VacationDays, out int vacationDays);
                    doctor.VacationDays = vacationDays;
                    doctorController.UpdateDoctor(doctor);
                    CustomMessageBox.Show(TranslationSource.Instance["VacationDaysUpdatedMessage"]);

                    SecretaryUI.GetInstance().Caption.Content = TranslationSource.Instance["HomePageListItem"];
                    SecretaryUI.GetInstance().MainFrame.NavigationService.Navigate(ViewHome.GetInstance());
                }
                else
                {
                    CustomMessageBox.Show(TranslationSource.Instance["FillFieldsMessage"]);
                    return;
                }
            }
            else
            {
                if (!IsValid())
                {
                    return;
                }
                Doctor doctor = Doctors[DoctorSelectedIndex];
                VacationPeriod vacationPeriod = CreateVacationFromUserInput();
                int.TryParse(VacationDays, out int vacationDays);

                doctor.VacationDays = vacationDays;
                doctor.VacationPeriods.Add(vacationPeriod);
                doctor.VacationDays -= (vacationPeriod.EndTime - vacationPeriod.StartTime).Days;
                doctorController.UpdateDoctor(doctor);

                CustomMessageBox.Show(TranslationSource.Instance["VacationCreatedMessage"]);

                SecretaryUI.GetInstance().Caption.Content = TranslationSource.Instance["HomePageListItem"];
                SecretaryUI.GetInstance().MainFrame.NavigationService.Navigate(ViewHome.GetInstance());
            }
        }

        private bool CanExecute_AddVacationCommand(object obj)
        {
            return true;
        }
        private VacationPeriod CreateVacationFromUserInput()
        {
            DateTime startDateAndTime = DateTime.Parse(SelectedDateTextStart);
            DateTime endDateAndTime = DateTime.Parse(SelectedDateTextEnd);

            VacationPeriod vacationPeriod = new VacationPeriod(startDateAndTime, endDateAndTime);

            return vacationPeriod;
        }

        private void Execute_QuitCommand(object obj)
        {
            SecretaryUI.GetInstance().Caption.Content = TranslationSource.Instance["HomePageListItem"];
            SecretaryUI.GetInstance().MainFrame.NavigationService.Navigate(ViewHome.GetInstance());
        }

        private bool CanExecute_QuitCommand(object obj)
        {
            return true;
        }

        private void Execute_DoctorChangedCommand(object obj)
        {
            Doctor doctor = Doctors[DoctorSelectedIndex];
            VacationDays = doctor.VacationDays.ToString();
        }

        private bool CanExecute_DoctorChangedCommand(object obj)
        {
            return true;
        }

        /*public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyRaised(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }*/

    }
}
