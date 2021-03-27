// File:    Specijalista.cs
// Author:  paracelsus
// Created: Monday, March 22, 2021 6:35:52 PM
// Purpose: Definition of Class Specijalista

using System;

namespace Model
{
   public class Specijalista : Lekar
   {
      private Specijalizacija specijalizacija;

        public Specijalista() : base()
        {
        }

        public Specijalista(string ime, string prezime, string jmbg, string korisnickoIme, string lozinka, string email, string telefon, Adresa adresa, int daniGodisnjegOdmora,Specijalizacija specijalizacija) :base(ime, prezime, jmbg, korisnickoIme, lozinka, email, telefon, adresa,daniGodisnjegOdmora)
        {
            this.specijalizacija = specijalizacija;
        }
    }
}