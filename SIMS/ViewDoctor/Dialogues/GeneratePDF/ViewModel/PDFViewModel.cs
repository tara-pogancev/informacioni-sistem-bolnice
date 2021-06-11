using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using SIMS.Commands;
using SIMS.Controller;
using SIMS.Model;
using SIMS.PacijentGUI.ViewModel;

namespace SIMS.ViewDoctor.Dialogues.GeneratePDF.ViewModel
{
    class GeneratePDFViewModel : ViewModelPatient
    {
        #region fields
        private Patient patient;
        private PatientController patientController = new PatientController();

        public String LabelPatientName { get; set; }
        public String LabelDoctorName { get; set; }

        public MyICommand<Window> AcceptCommand { get; set; }

        private DateTime startDate;
        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                startDate = value;
                OnPropertyChanged();
            }
        }

        private DateTime endDate;
        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                endDate = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region constructor

        public GeneratePDFViewModel(Patient patientPar)
        {
            patient = patientController.GetPatient(patientPar.Jmbg);

            LabelDoctorName = DoctorUI.GetInstance().GetUser().FullName;
            LabelPatientName = patient.FullName;

            StartDate = DateTime.Today.AddDays(-30);
            EndDate = DateTime.Today;

            AcceptCommand = new MyICommand<Window>(Execute_AcceptCommand);
        }

        #endregion

        #region actions

        public void Execute_AcceptCommand(Window Window)
        {
            Accept(Window);
        }

        #endregion

        private void Accept(Window Window)
        {
            if (ValidateForm())
            {
                new PDFReport(patient, StartDate, EndDate);
                Window.Close();
                MessageBox.Show("Izveštaj uspešno sačuvan!");
            }
            else
                MessageBox.Show("Odabrani datumi nisu validni!", "Upozorenje!");
        }

        private bool ValidateForm()
        {
            return StartDate <= EndDate;
        }


    }

}
