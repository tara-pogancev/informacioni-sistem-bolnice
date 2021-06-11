using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Model
{
    public class Allergen
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public bool Serialize { get; set; }
        public Allergen()
        {
            ID = "";
            Name = "";
        }

        public Allergen(string ID, string Naziv)
        {
            this.ID = ID;
            this.Name = Naziv;
        }

        public bool shouldSerializeName()
        {
            return Serialize;
        }
    }
}
