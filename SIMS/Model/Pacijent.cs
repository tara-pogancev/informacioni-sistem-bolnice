// File:    Pacijent.cs
// Author:  paracelsus
// Created: Monday, March 22, 2021 6:35:53 PM
// Purpose: Definition of Class Pacijent

using System;
using System.Collections.Generic;

namespace Model
{
    public class Pacijent : UlogovanKorisnik
    {
        private String lbo;
        private bool gost;
        private List<string> alergeni;

        public string Lbo { get => lbo; set => lbo = value; }
        public bool Gost { get => gost; set => gost = value; }

        public Pacijent(string ime, string prezime, string jmbg, string korisnickoIme, string lozinka, string email, string telefon, Adresa adresa, String lbo, Boolean gost) : base(ime, prezime, jmbg, korisnickoIme, lozinka, email, telefon, adresa)
        {
            this.lbo = lbo;
            this.gost = gost;
        }

        public Pacijent() : base()
        {
        }

        public String GetGost
        {
            get
            {
                String ret = "";
                if (Gost)
                {
                    ret = "Da";
                }
                else
                {
                    ret = "Ne";
                }

                return ret;
            }

        }

        public void SetAttributes(Pacijent p)
        {
            this.Ime = p.Ime;
            this.Prezime = p.Prezime;
            this.Jmbg = p.Jmbg;
            this.KorisnickoIme = p.KorisnickoIme;
            this.Lozinka = p.Lozinka;
            this.Email = p.Email;
            this.Telefon = p.Telefon;
            this.Adresa = p.Adresa;
            this.Lbo = p.Lbo;
            this.Gost = p.Gost;
        }

    }
}