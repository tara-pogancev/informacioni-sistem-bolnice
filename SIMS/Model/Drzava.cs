

using System;

namespace Model
{
   public class Drzava
   {
      private String naziv;

        public Drzava()
        {

        }
      
        public Drzava(string naziv)
        {
            this.naziv = naziv;
        }

        public string Naziv { get => naziv; set => naziv = value; }

    }
}