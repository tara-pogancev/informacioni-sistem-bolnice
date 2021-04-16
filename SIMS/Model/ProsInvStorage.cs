using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Model
{
    class ProsInvStorage : Storage<string, ProsInv, ProsInvStorage>
    {
        protected override string getKey(ProsInv entity)
        {
            return entity.BrojProstorije + "_" + entity.IdInventara;
        }

        protected override string getPath()
        {
            return @".\..\..\..\Data\prosinv.json";
        }

        protected override void RemoveReferences(string key)
        {

        }

        public ProsInv Read(string brojProstorije, string idInventara)
        {
            var ret = Read(brojProstorije + "_" + idInventara);
            if(ret == null)
            {
                ret = new ProsInv(brojProstorije, idInventara, 0);
                Create(ret);
            }
            return ret;
        }
    }
}
