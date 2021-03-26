// File:    Lekar.cs
// Author:  paracelsus
// Created: 22 March 2021 19:32:13
// Purpose: Definition of Class Lekar

using System;

namespace P1
{
   public class Lekar : UlogovanKorisnik
   {
      private Boolean dostupnost;
      private int brojSlobodnihDana;

        public Lekar(string ime, string prezime, string jmbg, string korisnickoIme, string lozinka, string email, string telefon, Adresa adresa,Boolean dostupnost,int brojSlobodnihDana) : base(ime, prezime, jmbg, korisnickoIme, lozinka, email, telefon, adresa)
        {
            this.dostupnost = dostupnost;
            this.brojSlobodnihDana = brojSlobodnihDana;
        }

        public Boolean Dostupnost
        {
            get { return dostupnost; }
            set { dostupnost = value; }
        }

        public int BrojSlobodnihDana
        {
            get { return brojSlobodnihDana; }
            set { brojSlobodnihDana = value;  }
        }
    }
}