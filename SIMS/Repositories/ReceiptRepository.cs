using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class ReceiptRepository : GenericFileRepository<string, Receipt, ReceiptRepository>
    {
        protected override string getKey(Receipt entity)
        {
            return entity.ReceptKey;
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
                if (r.Pacijent.Jmbg == p.Jmbg)
                    retVal.Add(r);
            }

            return retVal;
        }

    }
}
