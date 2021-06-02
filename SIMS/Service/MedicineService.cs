using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Model;
using SIMS.DTO;
using SIMS.Repositories.MedicationRepo;

namespace SIMS.Service
{
    public class MedicineService
    {
        private IMedicationRepository medicationRepository = new MedicationFileRepository();

        public List<Medication> GetAllMedicine() => medicationRepository.GetAll();

        public void UpdateMedicine(Medication medication) => medicationRepository.Update(medication);

        public void DeleteMedicine(Medication medication) => medicationRepository.Delete(medication.MedicineID);

        public void SaveMedicine(Medication medication) => medicationRepository.Save(medication);

        public void DeleteMedicine(string ID) => medicationRepository.Delete(ID);

        public Medication GetMedicine(String key) => medicationRepository.FindById(key);

        public List<Medication> GetApprovedMedicine()
        {
            return medicationRepository.GetApprovedMedicine();
        }

        public List<Medication> GetMedicineWaitingForApproval()
        {
            return medicationRepository.GetMedicineWaitingForApproval();
        }

        public List<Medication> GetDeniedMedicine()
        {
            return medicationRepository.GetDeniedMedicine();
        }

    }
}
