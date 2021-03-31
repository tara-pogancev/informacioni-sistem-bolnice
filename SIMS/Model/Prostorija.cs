// File:    Prostorija.cs
// Author:  paracelsus
// Created: Monday, March 22, 2021 6:41:35 PM
// Purpose: Definition of Class Prostorija

using System;

namespace Model
{
   public class Prostorija
   {
      private int sprat;
      private String naziv;
      private bool dostupna;
      private TipProstorije tipProstorije;

        public Prostorija()
        {
        }

        public Prostorija(int sprat, String naziv, bool dostupna, TipProstorije tipProstorije)
        {
            this.sprat = sprat;
            this.naziv = naziv;
            this.dostupna = dostupna;
            this.tipProstorije = tipProstorije;
        }

        public int Sprat { get => sprat; set => sprat = value; }
        public String Naziv { get => naziv; set => naziv = value; }
        public bool Dostupna { get => dostupna; set => dostupna = value; }
        public TipProstorije TipProstorije { get => tipProstorije; set => tipProstorije = value; }

    }
}