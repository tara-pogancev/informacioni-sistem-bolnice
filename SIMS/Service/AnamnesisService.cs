using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Model;
using SIMS.Repositories.AnamnesisRepository;
using SIMS.Repositories.SecretaryRepo;

namespace SIMS.Service
{
    public class AnamnesisService
    {
        private IAnamnesisRepository anamnesisRepository;

        public AnamnesisService()
        {
            anamnesisRepository = new AnamnesisFileRepository();
        }

        public List<Anamnesis> GetAnamnesisByDoctor(Doctor doctor)
        {
            List<Anamnesis> retVal = new List<Anamnesis>();

            foreach (Anamnesis anamnesis in anamnesisRepository.GetAll())
            {
                if (anamnesis.GetAppointment().Doctor.Jmbg == doctor.Jmbg)
                    retVal.Add(anamnesis);

            }

            return retVal;
        }

        public List<Anamnesis> GetAnamnesisByPatient(Patient patient)
        {
            List<Anamnesis> retVal = new List<Anamnesis>();

            foreach (Anamnesis anamnesis in anamnesisRepository.GetAll())
            {
                if (anamnesis.GetAppointment().Patient.Jmbg == patient.Jmbg)
                    retVal.Add(anamnesis);

            }

            return retVal;
        }

    }
}
