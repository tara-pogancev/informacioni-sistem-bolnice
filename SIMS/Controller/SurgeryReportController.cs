using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Model;
using SIMS.Service;

namespace SIMS.Controller
{
    public class SurgeryReportController
    {
        private SurgeryReportService surgeryReportService = new SurgeryReportService();

        public void UpdateReport(SurgeryReport report) => surgeryReportService.UpdateReport(report);

        public void DeleteReport(String reportID) => surgeryReportService.DeleteReport(reportID);

        public void DeleteReport(SurgeryReport report) => surgeryReportService.DeleteReport(report.ReportID);

        public void SaveReport(SurgeryReport report) => surgeryReportService.SaveReport(report);

        public SurgeryReport GetReport(String reportID) => surgeryReportService.GetReport(reportID);

        public List<SurgeryReport> ReadByPatient(Patient patient)
        {
            return surgeryReportService.ReadByPatient(patient);
        }


    }
}
