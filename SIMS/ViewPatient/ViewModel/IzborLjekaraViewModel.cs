using SIMS.Repositories.SecretaryRepo;
using SIMS.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Controller;
using SIMS.Model;

namespace SIMS.PacijentGUI.ViewModel
{
    public class IzborLjekaraViewModel : ViewModelPatient
    {
        # region fields

        private Patient patient;

        private Doctor chosenDoctor;

        private String nameSurname;

        private String date;

        private double grade;

        private String phoneNumber;

      

        public String NameSurname {
            get { return nameSurname; }
            set
            {
                nameSurname = value;
                OnPropertyChanged();
            }
        }
        public Doctor ChosenDoctor{
            get { return chosenDoctor; }
            set
            {
                chosenDoctor = value;
                OnPropertyChanged();
            }
        }

        public String Date
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged();
            }
        }

        public String PhoneNumber
        {
            get { return phoneNumber; }
            set
            {
                phoneNumber = value;
                OnPropertyChanged();
            }
        }
        
        public double Grade
        {
            get { return grade; }
            set
            {
                grade = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region actions
        public bool CanExecute_ChoseDoctorCommand(object obj)
        {
            return true;
        }

        public void Execute_ChoseDoctrCommand(object obj)
        {
            patient.ChosenDoctor = chosenDoctor;
        }

        public bool CanExecute_DetailsDoctorCommand(object obj)
        {
            return true;
        }

        public void Execute_DetailsDoctrCommand(object obj)
        {

        }

        #endregion

        #region constructors

        public IzborLjekaraViewModel(Patient patient)
        {
            this.patient = patient;
            Doctor doctor = patient.ChosenDoctor;
            this.nameSurname = doctor.FullName;
            this.PhoneNumber = "221-537";
            new DoctorController().RecalulateGrade(doctor);
            this.grade = doctor.Grade;
            this.date = "13.2.1975.";

        }

        #endregion





    }
}
