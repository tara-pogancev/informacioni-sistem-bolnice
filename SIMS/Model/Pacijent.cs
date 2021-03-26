// File:    Pacijent.cs
// Author:  paracelsus
// Created: 22 March 2021 18:35:53
// Purpose: Definition of Class Pacijent

using System;

namespace P1
{
   public class Pacijent : UlogovanKorisnik
   {
      private String lbo;
      private bool gost;
      private int krvnaGrupa;

        public Pacijent(string ime, string prezime, string jmbg, string korisnickoIme, string lozinka, string email, string telefon, Adresa adresa,String lbo,Boolean gost,int krvnaGrupa) : base(ime, prezime, jmbg, korisnickoIme, lozinka, email, telefon, adresa)
        {
            this.lbo = lbo;
            this.gost = gost;
            this.krvnaGrupa = krvnaGrupa;
        }

        public String Lbo
        {
            get { return lbo; }
            set { lbo = value; }
        }

        public Boolean Gost
        {
            get { return gost; }
            set { gost = value; }
        }

        public int KrvnaGrupa
        {
            get { return krvnaGrupa; }
            set { krvnaGrupa = value; }
        }
    }
}