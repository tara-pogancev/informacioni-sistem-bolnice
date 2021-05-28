using SIMS.Repositories.SecretaryRepo;
using SIMS.Commands;
using System;
using System.Collections.Generic;
using System.Text;
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

        private String grade;

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
        
        public String Grade
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
            this.nameSurname = "Tara Pogancev";
            this.PhoneNumber = "221-537";
            this.grade = "5";
            this.date = "22.3.1970.";
            
        }

        #endregion





    }
}
