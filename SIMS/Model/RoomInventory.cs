using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Repositories.SecretaryRepo;

namespace SIMS.Model
{
    public class RoomInventory
    {
        public string RoomNumber { get; set; }
        public string ID { get; set; }
        public int Quantity { get; set; }

        public RoomInventory()
        {

        }
        public RoomInventory(string brojProstorije, string idInventara, int kolicina)
        {
            this.RoomNumber = brojProstorije;
            this.ID = idInventara;
            this.Quantity = kolicina;
        }
    }
}
