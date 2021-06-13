using System;
using System.Collections.Generic;
using System.Text;
using SIMS.DTO;
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

        public List<Anamnesis> GetAllAnamnesis() => anamnesisRepository.GetAll();

        public void UpdateAnamnesis(Anamnesis anamnesis) => anamnesisRepository.Update(anamnesis);

        public void DeleteAnamnesis(Anamnesis anamnesis) => anamnesisRepository.Delete(anamnesis.AnamnesisID);

        public void SaveAnamnesis(Anamnesis anamnesis) => anamnesisRepository.Save(anamnesis);

        public Anamnesis GetAnamnesis(String key) => anamnesisRepository.FindById(key);

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
                if (anamnesis.GetAppointment() != null)
                    if (anamnesis.GetAppointment().Patient.Jmbg == patient.Jmbg)
                        retVal.Add(anamnesis);

            }

            return retVal;
        }

        public AnamnesisDTO GetDTO(Anamnesis anamnesis)
        {
            return new AnamnesisDTO(anamnesis);
        }

        public List<AnamnesisDTO> GetDTOFromList(List<Anamnesis> list)
        {
            List<AnamnesisDTO> retVal = new List<AnamnesisDTO>();
            foreach (Anamnesis anamnesis in list)
                retVal.Add(GetDTO(anamnesis));

            return retVal;
        }

        public List<Anamnesis> GetListForPatientByDate(Patient patient, DateTime startDate, DateTime endDate)
        {
            List<Anamnesis> retVal = new List<Anamnesis>();

            foreach (Anamnesis anamnesis in GetAnamnesisByPatient(patient))
                if (anamnesis.AnamnesisDate >= startDate && anamnesis.AnamnesisDate <= endDate)
                    retVal.Add(anamnesis);
            
            return retVal;
        }

    }
}
