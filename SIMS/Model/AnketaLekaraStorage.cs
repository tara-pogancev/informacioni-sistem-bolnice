using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace SIMS.Model
{
    class AnketaLekaraStorage : Storage<string, AnketaLekara, AnketaLekaraStorage>
    {
        protected override string getKey(AnketaLekara entity)
        {
            return entity.Termin.TerminKey;
        }

        protected override string getPath()
        {
             return @".\..\..\..\Data\anketaLekara.json";
        }

        protected override void RemoveReferences(string key)
        {
            throw new NotImplementedException();
        }
    }
}
