using Model;
using SIMS.PacijentGUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Model
{
    class AnketaBolniceStorage : Storage<string, AnketaBolnice, AnketaBolniceStorage>
    {
        protected override string getKey(AnketaBolnice entity)
        {
            return entity.IdAnkete;
        }

        protected override string getPath()
        {
            return @".\..\..\..\Data\anketaBolnice.json";
        }

        protected override void RemoveReferences(string key)
        {
            throw new NotImplementedException();
        }

        public List<AnketaBolnice> getAnketeByPatient(Pacijent pacijent)
        {
            List<AnketaBolnice> anketeBolnice = ReadList();
            for(int i = 0; i < anketeBolnice.Count; i++)
            {
                if (anketeBolnice[i].IdVlasnika != pacijent.Jmbg)
                {
                    anketeBolnice.RemoveAt(i);
                    i--;
                }
            }
            return anketeBolnice;
        } 


    }
}
