using SIMS.Controller;
using SIMS.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.ViewPatient.ViewModel
{
    public class DoctorDetailsViewModel
    {
        public Doctor SelectedDoctor { get; set; }
        public List<DoctorSurvey> DoctorSurveys { get; set; }
        public Doctor Doctor { get; set; }

        public DoctorDetailsViewModel(Doctor selectedDoctor)
        {
            SelectedDoctor = selectedDoctor;
            Doctor = new DoctorController().GetDoctor(SelectedDoctor.Jmbg);
            DoctorSurveys = new DoctorSurveyController().GetSurveysForDoctor(Doctor);
            new DoctorController().RecalulateGrade(Doctor);
        }

       
    }
}
