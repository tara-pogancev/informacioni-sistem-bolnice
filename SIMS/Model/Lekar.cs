// File:    Lekar.cs
// Author:  paracelsus
// Created: Monday, March 22, 2021 7:32:13 PM
// Purpose: Definition of Class Lekar

using System;

namespace Model
{
   public class Lekar : UlogovanKorisnik
   {
      private int daniGodisnjegOdmora;

        public Lekar()
        {
        }

        public Lekar(string ime, string prezime, string jmbg, string korisnickoIme, string lozinka, string email, string telefon, Adresa adresa,int daniGodisnjegOdmora) : base(ime, prezime, jmbg, korisnickoIme, lozinka, email, telefon, adresa)
        {
            this.daniGodisnjegOdmora = daniGodisnjegOdmora;
        }

        public int DaniGodisnjegOdmora { get => daniGodisnjegOdmora; set => daniGodisnjegOdmora = value; }
    }
}