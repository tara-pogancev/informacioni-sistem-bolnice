// File:    Specijalista.cs
// Author:  paracelsus
// Created: 22 March 2021 18:35:52
// Purpose: Definition of Class Specijalista

using System;

namespace P1
{
   public class Specijalista : Lekar
   {
      private Specijalizacija specijalizacija;

        public Specijalista(string ime, string prezime, string jmbg, string korisnickoIme, string lozinka, string email, string telefon, Adresa adresa, bool dostupnost, int brojSlobodnihDana) : base(ime, prezime, jmbg, korisnickoIme, lozinka, email, telefon, adresa, dostupnost, brojSlobodnihDana)
        {
            specijalizacija = Specijalizacija.hirurg;
        }
    }
}