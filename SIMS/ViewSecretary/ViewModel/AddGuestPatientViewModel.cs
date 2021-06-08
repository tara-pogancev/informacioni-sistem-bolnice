using SIMS.Commands;
using SIMS.Controller;
using SIMS.Model;
using SIMS.ViewSecretary.Patients;
using System;
using System.Collections.Generic;
using System.Text;

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
            Patient patient = new Patient(Name, LastName, Jmbg);
            patientController.SavePatient(patient);
            ViewPatients.GetInstance().RefreshView();

            SecretaryUI.GetInstance().MainFrame.NavigationService.GoBack();
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
    }
}
