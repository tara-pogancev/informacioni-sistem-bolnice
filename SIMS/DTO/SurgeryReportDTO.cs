using SIMS.Controller;
using SIMS.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.DTO
{
    class SurgeryReportDTO
    {
        private AppointmentController appointmentController = new AppointmentController();
        private SurgeryReportController surgeryReportController = new SurgeryReportController();

        public String ReportID { get; set; }
        public String DateOfSurgery { get => Surgery.GetAppointmentDate(); }
        public String SurgeryName { get => Report.SurgeryName; }
        public String DoctorName { get; set; }
        public Appointment Surgery { get; set; }
        public SurgeryReport Report { get; set; }

        public SurgeryReportDTO(SurgeryReport report)
        {
            ReportID = report.ReportID;
            Surgery = appointmentController.GetAppointment(ReportID);
            Report = surgeryReportController.GetReport(ReportID);
            Surgery.InitData();
        }
    }
}
