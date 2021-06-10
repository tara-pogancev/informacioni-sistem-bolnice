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
        private DateTime selectedDateStart;
        public DateTime SelectedDateStart
        {
            get
            {
                return selectedDateStart;
            }
            set
            {
                selectedDateStart = value;
                SelectedDateEnd = selectedDateStart;
            }
        }
        public string SelectedDateTextStart { get; set; }
        private DateTime selectedDateEnd;
        public DateTime SelectedDateEnd
        {
            get
            {
                return selectedDateEnd;
            }
            set
            {
                selectedDateEnd = value;
                OnPropertyChanged("SelectedDateEnd");
            }
        }
        public string SelectedDateTextEnd { get; set; }
        public List<string> Times { get; set; }
        public string SelectedTimeStart { get; set; }
        public string SelectedTimeEnd { get; set; }

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
            Times = new List<string>() { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00" };
            SelectedTimeStart = Times[0];
            SelectedTimeEnd = Times[0];

        }

        private bool IsValid(VacationPeriod vacationPeriod)
        {
            string strRegex = @"[0-9]+$";

            Regex re = new Regex(strRegex);
            if (String.IsNullOrEmpty(VacationDays) || VacationDays.Trim().Equals("") || !re.IsMatch(VacationDays))
            {
                CustomMessageBox.Show(TranslationSource.Instance["VacationNotNumberMessage"]);
                return false;
            }
            if (vacationPeriod.EndTime <= vacationPeriod.StartTime)
            {
                CustomMessageBox.Show(TranslationSource.Instance["InvalidDatesMessage"]);
                return false;
            }
            if ((vacationPeriod.EndTime - vacationPeriod.StartTime).Days > DoctorToUpdate.VacationDays)
            {
                CustomMessageBox.Show(TranslationSource.Instance["NotEnoughVacationDaysMessage"]);
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
                VacationPeriod vacationPeriod = CreateVacationFromUserInput();
                if (!IsValid(vacationPeriod))
                {
                    return;
                }
                Doctor doctor = Doctors[DoctorSelectedIndex];

                int.TryParse(VacationDays, out int vacationDays);

                doctor.VacationDays = vacationDays;
                doctor.VacationPeriods.Add(vacationPeriod);
                if (vacationPeriod.StartTime.Date == vacationPeriod.EndTime.Date)
                {
                    doctor.VacationDays--;
                }
                else
                {
                    doctor.VacationDays -= (vacationPeriod.EndTime - vacationPeriod.StartTime).Days;
                }

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
            DateTime startDateAndTime = DateTime.Parse(SelectedDateTextStart + " " + SelectedTimeStart);
            DateTime endDateAndTime = DateTime.Parse(SelectedDateTextEnd + " " + SelectedTimeEnd);

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
    }
}
