// File:    Upravnik.cs
// Author:  paracelsus
// Created: Monday, March 22, 2021 6:35:50 PM
// Purpose: Definition of Class Upravnik

using System;

namespace SIMS.Repositories.PatientRepo
{
   public class Manager : LoggedUser
   {
      private int daniGodisnjegOdmora;
        

        public Manager()
        {
        }

        public Manager(string ime, string prezime, string jmbg, string korisnickoIme, string lozinka, string email, string telefon, Address adresa,int daniGodisnjegOdmora) : base(ime, prezime, jmbg, korisnickoIme, lozinka, email, telefon, adresa)
        {
            this.daniGodisnjegOdmora = daniGodisnjegOdmora;
        }

        public int DaniGodisnjegOdmora { get => daniGodisnjegOdmora; set => daniGodisnjegOdmora = value; }

        public bool ShouldSerializeDaniGodisnjegOdmora()
        {
            return serijalizuj;
        }
    }

}