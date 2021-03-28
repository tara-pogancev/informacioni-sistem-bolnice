// File:    Upravnik.cs
// Author:  paracelsus
// Created: Monday, March 22, 2021 6:35:50 PM
// Purpose: Definition of Class Upravnik

using System;

namespace Model
{
   public class Upravnik : UlogovanKorisnik
   {
      private int daniGodisnjegOdmora;

        public Upravnik()
        {
        }

        public Upravnik(string ime, string prezime, string jmbg, string korisnickoIme, string lozinka, string email, string telefon, Adresa adresa,int daniGodisnjegOdmora) : base(ime, prezime, jmbg, korisnickoIme, lozinka, email, telefon, adresa)
        {
            this.daniGodisnjegOdmora = daniGodisnjegOdmora;
        }

        public int DaniGodisnjegOdmora { get => daniGodisnjegOdmora; set => daniGodisnjegOdmora = value; }
    }

}