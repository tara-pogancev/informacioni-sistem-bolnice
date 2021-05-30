using System;
using System.Collections.Generic;
using System.Text;
using SIMS.DTO;
using SIMS.Model;
using SIMS.Repositories.SurgeryReportRepo;

namespace SIMS.Service
{
    public class SurgeryReportService
    {
        private ISurgeryReportRepository surgeryReportRepository = new SurgeryReportFileRepository();

        public SurgeryReportService()
        {

        }

        public void UpdateReport(SurgeryReport report) => surgeryReportRepository.Update(report);

        public void DeleteReport(String reportID) => surgeryReportRepository.Delete(reportID);

        public void DeleteReport(SurgeryReport report) => surgeryReportRepository.Delete(report.ReportID);

        public void SaveReport(SurgeryReport report) => surgeryReportRepository.Save(report);

        public SurgeryReport GetReport(String reportID) => surgeryReportRepository.FindById(reportID);

        public List<SurgeryReport> ReadByPatient(Patient patient)
        {
            return surgeryReportRepository.ReadByPatient(patient);
        }

        public SurgeryReportDTO GetDTO(SurgeryReport report)
        {
            return new SurgeryReportDTO(report);
        }

        public List<SurgeryReportDTO> GetDTOFromList(List<SurgeryReport> list)
        {
            List<SurgeryReportDTO> retVal = new List<SurgeryReportDTO>();
            foreach (SurgeryReport report in list)
                retVal.Add(GetDTO(report));

            return retVal;
        }

    }
}
