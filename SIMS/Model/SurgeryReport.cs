using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using SIMS.Repositories.AppointmentRepo;
using SIMS.Repositories.DoctorRepo;

namespace SIMS.Model
{
    public class SurgeryReport
    {
        public DateTime DateOfReport { get; set; }
        public String ReportID { get; set; }
        public String SurgeryName { get; set; }
        public String SurgeryDescription { get; set; }

        public SurgeryReport()
        {
            DateOfReport = DateTime.Today;
        }

        public SurgeryReport(Appointment surgery, String surgeryName, String surgeryDescription)
        {
            DateOfReport = DateTime.Today;
            SurgeryName = surgeryName;
            SurgeryDescription = surgeryDescription;
            ReportID = surgery.AppointmentID;
        }

        public Appointment GetSurgery()
        {
            return new AppointmentFileRepository().FindById(ReportID);
        }

    }
}
