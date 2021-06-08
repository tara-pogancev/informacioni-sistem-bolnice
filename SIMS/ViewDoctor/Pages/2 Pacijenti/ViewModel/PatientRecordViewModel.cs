using SIMS.Commands;
using SIMS.Controller;
using SIMS.LekarGUI;
using SIMS.LekarGUI.Dialogues.Hospitalizacija;
using SIMS.LekarGUI.Dialogues.Recepti_i_terapije;
using SIMS.Model;
using SIMS.PacijentGUI.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.ViewDoctor.Pages._2_Pacijenti.ViewModel
{
    class PatientRecordViewModel : ViewModelPatient
    {
        #region fields
        private Patient patient;
        private HospitalizationController hospitalizationController = new HospitalizationController();
        private PatientController patientController = new PatientController();

        public String LabelFullName { get; set; }
        public String LabelGender { get; set; }
        public String LabelJMBG { get; set; }
        public String LabelLBO { get; set; }
        public String LabelPhone { get; set; }
        public String LabelDateOfBirth { get; set; }
        public String LabelEmail { get; set; }
        public String LabelAddress { get; set; }
        public String LabelBloodType { get; set; }
        public String LabelAllergens { get; set; }
        public String LabelHronical { get; set; }

        #endregion

        #region commands

        public RelayCommand Hospitalization { get; set; }
        public RelayCommand ViewDocumentation { get; set; }
        public RelayCommand WriteTherapy { get; set; }
        public RelayCommand WriteReceipt { get; set; }

        #endregion


        #region constructors

        public PatientRecordViewModel(Patient patientPar)
        {
            patient = patientController.GetPatient(patientPar.Jmbg);

            LabelFullName = this.patient.FullName;

            LabelGender = "Pol: " + this.patient.GetGenderString();
            LabelDateOfBirth = "Datum rođenja: " + this.patient.GetDateOfBirthString();
            LabelJMBG = "JMBG: " + this.patient.Jmbg;
            LabelLBO = "LBO: " + this.patient.Lbo;

            LabelPhone = "Broj telefona: " + this.patient.Phone;
            LabelEmail = "Email: " + this.patient.Email;
            LabelAddress = "Adresa: " + this.patient.FullAddressString;

            LabelBloodType = "Krvna grupa: " + this.patient.GetBloodTypeString();
            LabelAllergens = "Alergeni: " + this.patient.GetAllergenListString();
            LabelHronical = "Hronične bolesti: " + this.patient.GetHronicalDiseases();

            Hospitalization = new RelayCommand(Execute_Hospitalization);
            ViewDocumentation = new RelayCommand(Execute_ViewDocumentation);
            WriteTherapy = new RelayCommand(Execute_WriteTherapy);
            WriteReceipt = new RelayCommand(Execute_WriteReceipt);

        }

        #endregion

        #region actions

        //Hospitalization
        public void Execute_Hospitalization(object obj)
        {
            if (hospitalizationController.GetIfPatientHospitalzied(patient))
                DoctorUI.GetInstance().SellectedTab.Content = new PatientHospitalizationPage(patient);
            else
                new HospitalizeCreate(patient).ShowDialog();
        }

        //Documentation
        public void Execute_ViewDocumentation(object obj)
        {
            DoctorUI.GetInstance().SellectedTab.Content = new PatientDocumentationView(patient);
        }

        //Therapy
        public void Execute_WriteTherapy(object obj)
        {
            new TherapyCreate().ShowDialog();
        }

        //Receipt
        public void Execute_WriteReceipt(object obj)
        {
            new DoctorWriteReceipt(patient).Show();
        }

        #endregion



    }
}
