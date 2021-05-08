using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    class LekStorage : Storage<string, Lek, LekStorage>
    {
        protected override string getKey(Lek entity)
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

        public List<Lek> getApprovedMedicine()
        {
            List<Lek> retVal = new List<Lek>();

            foreach (Lek medicine in ReadList())
            {
                if (medicine.ApprovalStatus == MedicineApprovalStatus.Accepted)
                    retVal.Add(medicine);
            }

            return retVal;
        }

        public List<Lek> getMedicineWaitingForApproval()
        {
            List<Lek> retVal = new List<Lek>();

            foreach (Lek medicine in ReadList())
            {
                if (medicine.ApprovalStatus == MedicineApprovalStatus.Waiting)
                    retVal.Add(medicine);
            }

            return retVal;
        }

        public List<Lek> getDeniedMedicine()
        {
            List<Lek> retVal = new List<Lek>();

            foreach (Lek medicine in ReadList())
            {
                if (medicine.ApprovalStatus == MedicineApprovalStatus.Denied)
                    retVal.Add(medicine);
            }

            return retVal;
        }


    }
}
