using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Repositories.SecretaryRepo
{
    public class ReceiptFileRepository : GenericFileRepository<string, Receipt, ReceiptFileRepository>,IReceiptRepository
    {
        protected override string getKey(Receipt entity)
        {
            return entity.RecieptKey;
        }

        protected override void RemoveReferences(string key)
        {
            //throw new NotImplementedException();
        }

        protected override string getPath()
        {
            return @".\..\..\..\Data\recepti.json";
        }

        public List<Receipt> ReadByPatient(Patient p)
        {
            List<Receipt> retVal = new List<Receipt>();

            foreach (Receipt r in this.GetAll())
            {
                if (r.Patient.Jmbg == p.Jmbg)
                    retVal.Add(r);
            }

            return retVal;
        }

    }
}
