// File:    Sekretar.cs
// Author:  paracelsus
// Created: Monday, March 22, 2021 6:35:51 PM
// Purpose: Definition of Class Sekretar

using System;

namespace Model
{
   public class Sekretar : UlogovanKorisnik
   {
      private int daniGodisnjegOdmora;

        public Sekretar() :base()
        {
        }

        public Sekretar(string ime, string prezime, string jmbg, string korisnickoIme, string lozinka, string email, string telefon, Adresa adresa,int daniGodisnjegOdmora) : base(ime, prezime, jmbg, korisnickoIme, lozinka, email, telefon, adresa)
        {
            this.daniGodisnjegOdmora = daniGodisnjegOdmora;
        }
    }
   
}