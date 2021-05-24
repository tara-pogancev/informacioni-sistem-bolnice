using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Repositories.SecretaryRepo;

namespace SIMS.Model
{
    class RoomInventory
    {
        public string BrojProstorije { get; set; }
        public string IdInventara { get; set; }
        public int Kolicina { get; set; }

        public RoomInventory()
        {

        }
        public RoomInventory(string brojProstorije, string idInventara, int kolicina)
        {
            this.BrojProstorije = brojProstorije;
            this.IdInventara = idInventara;
            this.Kolicina = kolicina;
        }
    }
}
