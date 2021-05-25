using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Model;
using SIMS.Service;

namespace SIMS.Controller
{
    class MedicineController
    {
        private MedicineService medicineService = new MedicineService();

        MedicineController()
        {

        }

        public List<Medication> GetAllMedicine() => medicineService.GetAllMedicine();

        public void UpdateMedicine(Medication medication) => medicineService.UpdateMedicine(medication);

        public void DeleteMedicine(Medication medication) => medicineService.DeleteMedicine(medication);

        public void SaveMedicine(Medication medication) => medicineService.SaveMedicine(medication);

        public Medication GetMedicine(String key) => medicineService.GetMedicine(key);

        public List<Medication> GetApprovedMedicine()
        {
            return medicineService.GetApprovedMedicine();
        }

        public List<Medication> GetMedicineWaitingForApproval()
        {
            return medicineService.GetMedicineWaitingForApproval();
        }

        public List<Medication> GetDeniedMedicine()
        {
            return medicineService.GetDeniedMedicine();
        }


    }
}
