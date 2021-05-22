// File:    Sekretar.cs
// Author:  paracelsus
// Created: Monday, March 22, 2021 6:35:51 PM
// Purpose: Definition of Class Sekretar

using System;

namespace SIMS.Repositories.PatientRepo
{
   public class Secretary : LoggedUser
   {
      private int daniGodisnjegOdmora;

        

        public Secretary() :base()
        {
        }

        public Secretary(string ime, string prezime, string jmbg, string korisnickoIme, string lozinka, string email, string telefon, Address adresa,int daniGodisnjegOdmora) : base(ime, prezime, jmbg, korisnickoIme, lozinka, email, telefon, adresa)
        {
            this.daniGodisnjegOdmora = daniGodisnjegOdmora;
        }

        public Secretary(Secretary s) : base(s.Ime, s.Prezime, s.Jmbg, s.KorisnickoIme, s.Lozinka, s.Email, s.Telefon, s.Adresa)
        {
            this.daniGodisnjegOdmora = s.daniGodisnjegOdmora;
        }

        public int DaniGodisnjegOdmora { get => daniGodisnjegOdmora; set => daniGodisnjegOdmora = value; }

        public bool ShouldSerializeDaniGodisnjegOdmora()
        {
            return serijalizuj;
        }
    }
   
}