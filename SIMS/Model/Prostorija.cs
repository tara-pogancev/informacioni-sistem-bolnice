// File:    Prostorija.cs
// Author:  paracelsus
// Created: 22 March 2021 18:41:35
// Purpose: Definition of Class Prostorija

using System;

namespace P1
{
   public class Prostorija
   {
      private Adresa adresa;
      private int sprat;
      private int broj;
      private Boolean dostupnost;
      private String naziv;

        public Prostorija(String naziv, Adresa adresa, int sprat, int broj, bool dostupnost)
        {
            this.naziv = naziv;
            this.adresa = adresa;
            this.sprat = sprat;
            this.broj = broj;
            this.dostupnost = dostupnost;
        }

        public String Naziv
        {
            get { return naziv; }
            set { naziv = value; }
        }

        public Adresa Adresa
        {
            get { return adresa; }
            set { adresa = value; }
        }

        public int Sprat
        {
            get { return sprat; }
            set { sprat = value; }
        }

        public int Broj
        {
            get { return broj; }
            set { broj = value; }
        }

        public Boolean Dostupnost
        {
            get { return dostupnost; }
            set { dostupnost = value; }
        }

    }


}