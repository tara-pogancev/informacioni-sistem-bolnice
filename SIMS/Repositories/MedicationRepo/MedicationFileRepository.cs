using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Model;

namespace SIMS.Repositories.MedicationRepo
{
    class MedicationFileRepository : GenericFileRepository<string, Medication, MedicationFileRepository>,IMedicationRepository
    {
        protected override string getKey(Medication entity)
        {
            return entity.MedicineID;
        }

        protected override string getPath()
        {
            return @".\..\..\..\Data\lekovi.json";
        }

        protected override void RemoveReferences(string key)
        {
        
        }

        public List<Medication> GetApprovedMedicine()
        {
            List<Medication> retVal = new List<Medication>();

            foreach (Medication medicine in GetAll())
            {
                if (medicine.ApprovalStatus == MedicineApprovalStatus.Accepted)
                    retVal.Add(medicine);
            }

            return retVal;
        }

        public List<Medication> GetMedicineWaitingForApproval()
        {
            List<Medication> retVal = new List<Medication>();

            foreach (Medication medicine in GetAll())
            {
                if (medicine.ApprovalStatus == MedicineApprovalStatus.Waiting)
                    retVal.Add(medicine);
            }

            return retVal;
        }

        public List<Medication> GetDeniedMedicine()
        {
            List<Medication> retVal = new List<Medication>();

            foreach (Medication medicine in GetAll())
            {
                if (medicine.ApprovalStatus == MedicineApprovalStatus.Denied)
                    retVal.Add(medicine);
            }

            return retVal;
        }

        protected override void ShouldSerialize(Medication entity)
        {
            //ne treba logika za serijalizaciju
        }
    }
}
