using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class OpremaStorage : Storage<string, Oprema, OpremaStorage>
    {
        protected override string getPath()
        {
            return @".\..\..\..\Data\oprema.json";
        }
        protected override string getKey(Oprema dinamickaOprema)
        {
            return dinamickaOprema.Id;
        }

        protected override void RemoveReferences(string key)
        {
            foreach (var prosInv in ProsInvStorage.Instance.ReadAll().Values)
            {
                if (prosInv.IdInventara == key)
                {
                    ProsInvStorage.Instance.Delete(prosInv);
                }
            }

            foreach (var command in PremestajOpremeCommandStorage.Instance.ReadAll().Values)
            {
                if (command.OpremaID == key)
                {
                    PremestajOpremeCommandStorage.Instance.Delete(command);
                }
            }
        }
    }
}
