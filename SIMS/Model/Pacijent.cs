// File:    Pacijent.cs
// Author:  paracelsus
// Created: Monday, March 22, 2021 6:35:53 PM
// Purpose: Definition of Class Pacijent

using System;

namespace Model
{
   public class Pacijent : UlogovanKorisnik
   {
      private String lbo;
      private bool gost;

        public string Lbo { get => lbo; set => lbo = value; }
        public bool Gost { get => gost; set => gost = value; }

        public Pacijent(string ime, string prezime, string jmbg, string korisnickoIme, string lozinka, string email, string telefon, Adresa adresa,String lbo,Boolean gost) : base(ime, prezime, jmbg, korisnickoIme, lozinka, email, telefon, adresa)
        {
            this.lbo = lbo;
            this.gost = gost;
        }

        public Pacijent() : base()
        {
        }
    }
}