using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Model
{
    class AppointmentLog
    {
        public string AppointmentLogID { get; set; }
        public string AppointmentID { get; set; }
        public string PatientID { get; set; }
        public DateTime DateOfChange { get; set; }
        public SurgeryType SurgeryType { get; set; }
        public bool Expired { get; set; }

        public AppointmentLog()
        {
            Expired = false;
        }
        public AppointmentLog(String appointmentLogID, String appointmentID, String patientID, 
            DateTime dateOfChange, SurgeryType surgeryType)
        {
            AppointmentLogID = appointmentLogID;
            AppointmentID = appointmentID;
            PatientID = patientID;
            DateOfChange = dateOfChange;
            SurgeryType = surgeryType;
            Expired = false;
        }

        public bool NotInLastTenDays()
        {
            return this.DateOfChange < DateTime.Now.AddDays(-10);
        }

    }
}
