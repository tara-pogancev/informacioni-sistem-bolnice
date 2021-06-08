using SIMS.Commands;
using SIMS.Controller;
using SIMS.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace SIMS.ViewDoctor.Dialogues.Izvestaji.ViewModel
{
    class ReadSurgeryReportViewModel
    {
        #region fields
        private PatientController patientController = new PatientController();

        public String DoctorName { get; set; }
        public String SurgeryDate { get; set; }
        public String PatientName { get; set; }
        public String PatientDateOfBirth { get; set; }
        public String RoomNumber { get; set; }
        public String SurgeryName { get; set; }
        public String SurgeryDescription { get; set; }

        #endregion

        #region constructor

        public ReadSurgeryReportViewModel(SurgeryReport report)
        {
            CloseCommand = new MyICommand<Window>(Execute_CloseCommand);
            Appointment appointment = report.GetSurgery();
            appointment.InitData();
            Patient patient = patientController.GetPatient(appointment.Patient.Jmbg);

            DoctorName = "Doktor: " + appointment.GetDoctorName();
            SurgeryDate = "Datum operacije: " + appointment.GetAppointmentDate();

            PatientName = "Pacijent: " + patient.FullName;
            PatientDateOfBirth = "Datum rođenja: " + patient.GetDateOfBirthString();

            RoomNumber = "Prostorija: " + appointment.Room.Number;

            SurgeryName = report.SurgeryName;
            SurgeryDescription = report.SurgeryDescription;

        }

        #endregion

        #region commands

        public MyICommand<Window> CloseCommand { get; set; }

        public void Execute_CloseCommand(Window Window)
        {
            Window.Close();
        }

        #endregion

    }
}
