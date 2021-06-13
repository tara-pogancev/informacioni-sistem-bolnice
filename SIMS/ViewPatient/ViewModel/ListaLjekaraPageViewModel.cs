using SIMS.Repositories.SecretaryRepo;
using SIMS.Repositories.DoctorRepo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using SIMS.Model;
using SIMS.Controller;
using SIMS.Commands;
using SIMS.ViewPatient;
using SIMS.ViewPatient.ViewModel;

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

        public RelayCommand BackCommand { get; set; }

        public ListaLjekaraPageViewModel()
        {
           
            Doctors = new ObservableCollection<Doctor>(doctorController.GetAllDoctors());
            ChoseDoctorCommand = new RelayCommand(Execute_ChoseDoctrCommand, CanExecute_ChoseDoctorCommand);
            DatailsDoctorCommand = new RelayCommand(Execute_DetailsDoctrCommand, CanExecute_DetailsDoctorCommand);
            BackCommand= new RelayCommand(Execute_BackCommand, CanExecute_DetailsDoctorCommand);
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
            ObavjestenjeOTerminu obavjestenje = new ObavjestenjeOTerminu();
            obavjestenje.TekstObavjestenja.Text = "Izabrani ljekar je promjenjen";
            obavjestenje.ShowDialog();
        }

        public bool CanExecute_DetailsDoctorCommand(object obj)
        {
            return true;
        }

        public void Execute_DetailsDoctrCommand(object obj)
        {
            DoctorDetailsViewModel doctorDetailsViewModel = new DoctorDetailsViewModel(SelectedDoctor);
            PocetnaStranica.getInstance().frame.Content = new DetaljiODoktoru(doctorDetailsViewModel);
        }
        public void Execute_BackCommand(object obj)
        {
            PocetnaStranica.getInstance().frame.Content = new IzborLjekara();
        }

        #endregion
    }
}
