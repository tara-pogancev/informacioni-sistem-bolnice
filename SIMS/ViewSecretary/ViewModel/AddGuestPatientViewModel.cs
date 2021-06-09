using SIMS.Commands;
using SIMS.Controller;
using SIMS.Model;
using SIMS.ViewSecretary.Patients;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SIMS.ViewSecretary.ViewModel
{
    public class AddGuestPatientViewModel : ViewModelSecretary
    {
        private PatientController patientController = new PatientController();
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Jmbg { get; set; }

        public RelayCommand AddGuestCommand { get; set; }
        public RelayCommand QuitCommand { get; set; }
        public AddGuestPatientViewModel()
        {
            QuitCommand = new RelayCommand(Execute_QuitCommand, CanExecute_QuitCommand);
            AddGuestCommand = new RelayCommand(Execute_AddGuestCommand, CanExecute_AddGuestCommand);
        }

        private void Execute_AddGuestCommand(object obj)
        {
            if (IsValid())
            {
                Patient patient = new Patient(Name, LastName, Jmbg);
                patientController.SavePatient(patient);
                ViewPatients.GetInstance().RefreshView();

                SecretaryUI.GetInstance().MainFrame.NavigationService.GoBack();
            }
        }

        private bool CanExecute_AddGuestCommand(object obj)
        {
            return true;
        }

        private void Execute_QuitCommand(object obj)
        {
            SecretaryUI.GetInstance().MainFrame.NavigationService.GoBack();
        }

        private bool CanExecute_QuitCommand(object obj)
        {
            return true;
        }

        private bool IsValid()
        {
            if (String.IsNullOrEmpty(Name) || String.IsNullOrEmpty(LastName) || String.IsNullOrEmpty(Jmbg))
            {
                CustomMessageBox.Show(TranslationSource.Instance["FillFieldsMessage"]);
                return false;
            }
            if (Name.Trim().Equals("") || LastName.Trim().Equals("") || Jmbg.Trim().Equals(""))
            {
                CustomMessageBox.Show(TranslationSource.Instance["FillFieldsMessage"]);
                return false;
            }
            string strRegex = @"[0-9]{13}$";

            Regex re = new Regex(strRegex);

            if (!re.IsMatch(Jmbg))
            {
                CustomMessageBox.Show(TranslationSource.Instance["JmbgNumberMessage"]);
                return false;
            }
            return true;
        }
    }
}
