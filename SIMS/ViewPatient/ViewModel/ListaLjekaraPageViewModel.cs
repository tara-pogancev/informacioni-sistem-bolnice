using SIMS.Repositories.SecretaryRepo;
using SIMS.Repositories.DoctorRepo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using SIMS.Model;
using SIMS.Controller;
using SIMS.Commands;

namespace SIMS.PacijentGUI.ViewModel
{
    public class ListaLjekaraPageViewModel
    {
        private Patient patient;
        public ObservableCollection<Doctor> Doctors { get; set; }
        private DoctorController doctorController = new DoctorController();
        public Doctor SelectedDoctor { get; set; }
        public RelayCommand ChoseDoctorCommand { get; set; }

        public RelayCommand DatailsDoctorCommand { get; set; }

        public ListaLjekaraPageViewModel()
        {
            IDoctorRepository doctorRepository = new DoctorFileRepository();
            Doctors = new ObservableCollection<Doctor>(doctorRepository.GetAll());
            ChoseDoctorCommand = new RelayCommand(Execute_ChoseDoctrCommand, CanExecute_ChoseDoctorCommand);
            DatailsDoctorCommand = new RelayCommand(Execute_DetailsDoctrCommand, CanExecute_DetailsDoctorCommand);
            patient = PocetnaStranica.getInstance().Pacijent;
            recalculateGrades();
        }

        private void recalculateGrades()
        {
            foreach(var doctor in Doctors)
            {
                doctorController.RecalulateGrade(doctor);
            }
        }

        #region actions
        public bool CanExecute_ChoseDoctorCommand(object obj)
        {
            return true;
        }

        public void Execute_ChoseDoctrCommand(object obj)
        {
            patient.ChosenDoctor = SelectedDoctor;
        }

        public bool CanExecute_DetailsDoctorCommand(object obj)
        {
            return true;
        }

        public void Execute_DetailsDoctrCommand(object obj)
        {

        }

        #endregion
    }
}
