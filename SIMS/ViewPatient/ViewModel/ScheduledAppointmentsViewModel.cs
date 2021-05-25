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

        public ScheduledAppointmentsViewModel(Patient patient)
        {
            this.patient = patient;
            
        }

        
    }
}
