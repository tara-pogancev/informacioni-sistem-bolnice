using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Repositories.PatientRepo
{
    public class Inventory
    {
        public string Naziv { get; set; }
        public string Id { get; set; }
        public InventoryType TipOpreme { get; set; }

        public string BrojProstorije { get; set; } //neophodno za bindovanje

        public Inventory()
        {
            Naziv = "";
            Id = "";
            TipOpreme = InventoryType.statička;
        }
        public Inventory(string naziv, string id, InventoryType TipOpreme)
        {
            this.Naziv = naziv;
            this.Id = id;
            this.TipOpreme = TipOpreme;
        }

        public int Kolicina
        {
            get
            {
                if (BrojProstorije == null)
                {
                    return 0;
                }
                return RoomInventoryRepository.Instance.Read(BrojProstorije, Id).Kolicina;
            }
        }

        public string TipToString
        {
            get
            {
                return Conversion.TipOpremeToString(TipOpreme);
            }
        }

    }
}
