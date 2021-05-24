using SIMS.Repositories.SecretaryRepo;
using SIMS.Repositories.DoctorRepo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using SIMS.Model;
using SIMS.Controller;

namespace SIMS.PacijentGUI.ViewModel
{
    class ListaLjekaraPageViewModel
    {
        public ObservableCollection<Doctor> Doctors { get; set; }
        private DoctorController doctorController = new DoctorController();
        

        public ListaLjekaraPageViewModel()
        {
            IDoctorRepository doctorRepository = new DoctorFileRepository();
            Doctors = new ObservableCollection<Doctor>(doctorRepository.GetAll());
            recalculateGrades();
        }

        private void recalculateGrades()
        {
            foreach(var doctor in Doctors)
            {
                doctorController.RecalulateGrade(doctor);
            }
        }
    }
}
