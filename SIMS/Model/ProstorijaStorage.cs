using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Model
{
    public class ProstorijaStorage : Storage<string, Prostorija, ProstorijaStorage>
    {
        protected override string getPath()
        {
            return @".\..\..\..\Data\prostorije.json";
        } 

        protected override string getKey(Prostorija entity)
        {
            return entity.Broj;
        }

        protected override void RemoveReferences(string key)
        {
            TerminStorage storageT = new TerminStorage();
            foreach (Termin t in storageT.ReadList())
            {
                if (t.Prostorija.Broj == key)
                {
                    storageT.Delete(t.TerminKey);
                }
            }

            foreach (var prosInv in ProsInvStorage.Instance.ReadAll().Values)
            {
                if (prosInv.BrojProstorije == key)
                {
                    ProsInvStorage.Instance.Delete(prosInv);
                }
            }

            foreach (var command in PremestajOpremeCommandStorage.Instance.ReadAll().Values)
            {
                if (command.DstID == key || command.SrcID == key)
                {
                    PremestajOpremeCommandStorage.Instance.Delete(command);
                }
            }
        }

        public List<Prostorija> UcitajProstorijeZaPreglede()
        {
            List<Prostorija> prostorije = ReadList();
            for (int i = 0; i < prostorije.Count; i++)
            {
                if (prostorije[i].TipProstorije != TipProstorije.zaPreglede || prostorije[i].Dostupna==false)
                {
                    prostorije.RemoveAt(i);
                    i--;
                }
            }
            return prostorije;
        }
    }
}