using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    class MedicationRepository : GenericFileRepository<string, Medication, MedicationRepository>
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

        public List<Medication> getApprovedMedicine()
        {
            List<Medication> retVal = new List<Medication>();

            foreach (Medication medicine in GetAll())
            {
                if (medicine.ApprovalStatus == MedicineApprovalStatus.Accepted)
                    retVal.Add(medicine);
            }

            return retVal;
        }

        public List<Medication> getMedicineWaitingForApproval()
        {
            List<Medication> retVal = new List<Medication>();

            foreach (Medication medicine in GetAll())
            {
                if (medicine.ApprovalStatus == MedicineApprovalStatus.Waiting)
                    retVal.Add(medicine);
            }

            return retVal;
        }

        public List<Medication> getDeniedMedicine()
        {
            List<Medication> retVal = new List<Medication>();

            foreach (Medication medicine in GetAll())
            {
                if (medicine.ApprovalStatus == MedicineApprovalStatus.Denied)
                    retVal.Add(medicine);
            }

            return retVal;
        }


    }
}
