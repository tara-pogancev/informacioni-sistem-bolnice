using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Repositories.PatientRepo
{
    public class Allergen
    {
        public string ID { get; set; }
        public string Name { get; set; }

        public Allergen()
        {
        }

        public Allergen(string ID, string Naziv)
        {
            this.ID = ID;
            this.Name = Naziv;
        }
    }
}
