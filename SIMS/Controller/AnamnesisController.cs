using SIMS.Model;
using SIMS.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Controller
{
    public class AnamnesisController
    {
        private AnamnesisService anamnesisService = new AnamnesisService();
        
        public AnamnesisController()
        {

        }

        public List<Anamnesis> GetAllAnamnesis() => anamnesisService.GetAllAnamnesis();

        public void UpdateAnamnesis(Anamnesis anamnesis) => anamnesisService.UpdateAnamnesis(anamnesis);

        public void DeleteAnamnesis(Anamnesis anamnesis) => anamnesisService.DeleteAnamnesis(anamnesis);

        public void SaveAnamnesis(Anamnesis anamnesis) => anamnesisService.SaveAnamnesis(anamnesis);

        public Anamnesis GetAnamnesis(String key) => anamnesisService.GetAnamnesis(key);

        public List<Anamnesis> GetAnamnesisByDoctor(Doctor doctor)
        {
            return anamnesisService.GetAnamnesisByDoctor(doctor);
        }

        public List<Anamnesis> GetAnamnesisByPatient(Patient patient)
        {
            return anamnesisService.GetAnamnesisByPatient(patient);
        }
    }
}
