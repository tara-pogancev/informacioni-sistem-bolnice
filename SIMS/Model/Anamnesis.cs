﻿using Newtonsoft.Json;
using SIMS.Repositories.AppointmentRepo;
using SIMS.Repositories.DoctorRepo;
using SIMS.Repositories.DoctorSurveyRepo;
using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Controller;


namespace SIMS.Model
{
    public class Anamnesis
    {
        private AppointmentController appointmentController = new AppointmentController();

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
        private bool serialize;

        
        public Appointment AnamnesisAppointment { get; set; }
        public bool Serialize { get => serialize; set => serialize = value; }

        public Anamnesis()
        {
            AnamnesisDate = DateTime.Today;
            serialize = true;
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
            serialize = true;

            SetDefaultEmptyFields();
        }

        public Anamnesis(Anamnesis anamnesis)
        {
            anamnesis.InitData();

            AnamnesisAppointment = anamnesis.AnamnesisAppointment;
            AnamnesisDate = anamnesis.AnamnesisDate;

            MainIssues = anamnesis.MainIssues;
            CurrentAnamnesis = anamnesis.CurrentAnamnesis;
            GeneralOccurrences = anamnesis.GeneralOccurrences;
            AnamnesisID = anamnesis.AnamnesisID;
            RespiratorySystem = anamnesis.RespiratorySystem;
            CardioSystem = anamnesis.CardioSystem;
            DigestiveSystem = anamnesis.DigestiveSystem;
            UroGenitalSystem = anamnesis.UroGenitalSystem;
            LocomotorSystem = anamnesis.LocomotorSystem;
            NervousSystem = anamnesis.NervousSystem;
            PastDiseases = anamnesis.PastDiseases;
            FamilyData = anamnesis.FamilyData;
            SocioEpiData = anamnesis.SocioEpiData;
            serialize = true;
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
            return appointmentController.GetAppointment(AnamnesisID);
        }

        public void InitData()
        {
            AnamnesisAppointment = GetAppointment();
            AnamnesisAppointment.InitData();
        }

        public bool ShouldSerializeMainIssues() { return serialize; }
        public bool ShouldSerializeCurrentAnamnesis() { return serialize; }
        public bool ShouldSerializeGeneralOccurrences() { return serialize; }
        public bool ShouldSerializeRespiratorySystem() { return serialize; }
        public bool ShouldSerializeCardioSystem() { return serialize; }
        public bool ShouldSerializeDigestiveSystem() { return serialize; }
        public bool ShouldSerializeUroGenitalSystem() { return serialize; }
        public bool ShouldSerializeLocomotorSystem() { return serialize; }
        public bool ShouldSerializeNervousSystem() { return serialize; }
        public bool ShouldSerializePastDiseases() { return serialize; }
        public bool ShouldSerializeFamilyData() { return serialize; }
        public bool ShouldSerializeSocioEpiData() { return serialize; }
        public bool ShouldSerializeAnamnesisDate() { return serialize; }
        
    }
}
