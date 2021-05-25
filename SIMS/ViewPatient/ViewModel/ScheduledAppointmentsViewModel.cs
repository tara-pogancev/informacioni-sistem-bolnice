using SIMS.Controller;
using SIMS.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SIMS.PacijentGUI.ViewModel
{
    class ScheduledAppointmentsViewModel
    {
        public ObservableCollection<Appointment> PatientAppointments { get; set; }
        private Patient patient;
        private AppointmentController appointmentController;

        public ScheduledAppointmentsViewModel(Patient patient)
        {
            this.patient = patient;
            appointmentController = new AppointmentController();
            PatientAppointments=new ObservableCollection<Appointment>(appointmentController.GetPatientAppointments(patient));
            LoadPatientAndDoctorData();
            
        }

        private void LoadPatientAndDoctorData()
        {
            foreach (Appointment appointment in PatientAppointments)
            {
                appointment.Patient = new PatientController().GetPatient(appointment.Patient.Jmbg);
                appointment.Doctor = new DoctorController().FindByIdDoctor(appointment.Doctor.Jmbg);
            }
        }


    }
}
