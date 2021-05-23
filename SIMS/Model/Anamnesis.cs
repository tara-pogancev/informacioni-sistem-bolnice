using Newtonsoft.Json;
using SIMS.Repositories.AppointmentRepo;
using SIMS.Repositories.DoctorRepo;
using SIMS.Repositories.DoctorSurveyRepo;
using System;
using System.Collections.Generic;
using System.Text;


namespace SIMS.Repositories.SecretaryRepo
{
    public class Anamnesis
    {
        public String MainIssues { get; set; }
        public String CurrentAnamnesis { get; set; }
        public String GeneralOccurrences { get; set; }
        public String RespiratorySystem { get; set; }
        public String CardioSystem { get; set; }
        public String DigestiveSystem { get; set; }
        public String UroGenitalSystem { get; set; }
        public String LocomotorSystem { get; set; }
        public String NervousSystem { get; set; }
        public String PastDiseases { get; set; }
        public String FamilyData { get; set; }
        public String SocioEpiData { get; set; }
        public DateTime AnamnesisDate { get; set; }
        public String AnamnesisID { get; set; }

        [JsonIgnore]
        public Appointment AnamnesisAppointment { get; set; }


        public Anamnesis()
        {
            AnamnesisDate = DateTime.Today;
        }

        public Anamnesis(Appointment appointment, String mainIssues, String currentAnamnesis, String generalOccurrences, String respiratorySystem, String cardioSystem,
            String digestiveSystem, String uroGenitalSystem, String locomotorSystem, String nervousSystem, String pastDiseases, String familyData, String socioEpiData)
        {

            AnamnesisAppointment = appointment;
            AnamnesisDate = DateTime.Today;

            MainIssues = mainIssues;
            CurrentAnamnesis = currentAnamnesis;
            GeneralOccurrences = generalOccurrences;
            AnamnesisID = appointment.AppointmentID;
            RespiratorySystem = respiratorySystem;
            CardioSystem = cardioSystem;
            DigestiveSystem = digestiveSystem;
            UroGenitalSystem = uroGenitalSystem;
            LocomotorSystem = locomotorSystem;
            NervousSystem = nervousSystem;
            PastDiseases = pastDiseases;
            FamilyData = familyData;
            SocioEpiData = socioEpiData;

            SetDefaultEmptyFields();

        }

        private void SetDefaultEmptyFields()
        {
            if (RespiratorySystem == "") RespiratorySystem = "/";
            if (CardioSystem == "") this.CardioSystem = "/";
            if (DigestiveSystem == "") DigestiveSystem = "/";
            if (UroGenitalSystem == "") UroGenitalSystem = "/";
            if (LocomotorSystem == "") LocomotorSystem = "/";
            if (NervousSystem == "") NervousSystem = "/";
            if (PastDiseases == "") PastDiseases = "/";
            if (FamilyData == "") FamilyData = "/";
            if (SocioEpiData == "") SocioEpiData = "/";
        }

        public Appointment GetAppointment()
        {
            return AppointmentFileRepository.Instance.FindById(AnamnesisAppointment.AppointmentID);
        }

        [JsonIgnore]
        public String DoctorName
        {
            get { return AnamnesisAppointment.DoctorName; }
        }

        [JsonIgnore]
        public String PatientName
        {
            get { return AnamnesisAppointment.PatientName; }
        }

        [JsonIgnore]
        public String Date
        {
            get { return AnamnesisDate.ToString("dd.MM.yyyy."); }
        }

        [JsonIgnore]
        public String AppointmentDateType
        {
            get
            {
                if (GetAppointment().TypeString.Equals(AppointmentType.examination))
                    return "Datum pregleda: " + GetAppointment().AppointmentDate;
                else return "Datum operacije: " + GetAppointment().AppointmentDate;

            }
        }

        public void InitData()
        {
            AnamnesisAppointment =  new AppointmentFileRepository().FindById(AnamnesisID);
            AnamnesisAppointment.Patient = new PatientFileRepository().FindById(AnamnesisAppointment.Patient.Jmbg);
            AnamnesisAppointment.Doctor = new DoctorFileRepository().FindById(AnamnesisAppointment.Doctor.Jmbg);
        }
    }
}
