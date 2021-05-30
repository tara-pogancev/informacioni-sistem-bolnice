﻿using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Model;
using SIMS.Repositories.HospitalizationRepo;

namespace SIMS.Service
{
    public class HospitalizationService
    {
        private IHospitalizationRepository hospitalizationRepository = new HospitalizationFileRepository();

        public HospitalizationService()
        {

        }

        public List<Hospitalization> GetAllHospitalizations() => hospitalizationRepository.GetAll();

        public void UpdateHospitalization(Hospitalization hospitalization) => hospitalizationRepository.Update(hospitalization);

        public void DeleteHospitalization(Hospitalization hospitalization) => hospitalizationRepository.Delete(hospitalization.HospitalizationID);

        public void SaveHospitalization(Hospitalization hospitalization) => hospitalizationRepository.Save(hospitalization);

        public Hospitalization GetHospitalization(String key) => hospitalizationRepository.FindById(key);

        public Boolean GetIfPatientHospitalzied(Patient patient)
        {
            foreach (Hospitalization hospitalization in GetCurrentHospitalizations())
            {
                if (hospitalization.Patient.Jmbg == patient.Jmbg)
                    return true;
            }

            return false;
        }

        public List<Hospitalization> GetCurrentHospitalizations()
        {
            List<Hospitalization> retVal = new List<Hospitalization>();

            foreach (Hospitalization hospitalization in GetAllHospitalizations())
            {
                if (hospitalization.StartDate <= DateTime.Today && hospitalization.EndDate >= DateTime.Today)
                    retVal.Add(hospitalization);
            }
                return retVal;
        }

        public Hospitalization GetPatientCurrentHospitalization(Patient patient)
        {
            foreach (Hospitalization hospitalization in GetCurrentHospitalizations())
            {
                if (hospitalization.Patient.Jmbg == patient.Jmbg)
                    return hospitalization;
            }

            return null;
        }

    }
}
