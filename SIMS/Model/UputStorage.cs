using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace SIMS.Model
{
    public class UputStorage : Storage<string, Uput, UputStorage>
    {
        protected override string getKey(Uput entity)
        {
            return entity.RefferalKey;
        }

        protected override string getPath()
        {
            return @".\..\..\..\Data\uputi.json";
        }

        protected override void RemoveReferences(string key)
        {
            throw new NotImplementedException();
        }
    }
}
