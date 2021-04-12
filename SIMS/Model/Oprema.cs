using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Oprema
    {
        public string Naziv { get; set; }
        public string Id { get; set; }
        public TipOpreme TipOpreme { get; set; }

        public string BrojProstorije { get; set; } //neophodno za bindovanje

        public Oprema()
        {
            Naziv = "";
            Id = "";
            TipOpreme = TipOpreme.statička;
        }
        public Oprema(string naziv, string id, TipOpreme TipOpreme)
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
                return ProstorijaStorage.Instance.Read(BrojProstorije).GetKolicinaOpreme(Id);
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
