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
        private DateTime datum_rodjenja;
        private Krvne_Grupe krvna_grupa;
        private Pol pol;
        private List<string> hronicne_bolesti;

        public string Lbo { get => lbo; set => lbo = value; }
        public bool Gost { get => gost; set => gost = value; }

        public Pacijent(string ime, string prezime, string jmbg, string korisnickoIme, string lozinka, string email, string telefon, Adresa adresa, String lbo, Boolean gost, List<string> alergeni) : base(ime, prezime, jmbg, korisnickoIme, lozinka, email, telefon, adresa)
        {
            this.lbo = lbo;
            this.gost = gost;
            this.alergeni = alergeni;
        }

        public Pacijent(string ime, string prezime, string jmbg, string korisnickoIme, string lozinka, string email, string telefon, Adresa adresa, String lbo, Boolean gost, List<string> alergeni, DateTime datum_rodjenja, Krvne_Grupe krvna_grupa, Pol pol, List<string> hronicne_bolesti) : base(ime, prezime, jmbg, korisnickoIme, lozinka, email, telefon, adresa)
        {
            this.lbo = lbo;
            this.gost = gost;
            this.alergeni = alergeni;
            this.datum_rodjenja = datum_rodjenja;
            this.krvna_grupa = krvna_grupa;
            this.pol = pol;
            this.hronicne_bolesti = hronicne_bolesti;
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
        public List<string> Alergeni
        {
            get
            {
                return alergeni;
            }
            set
            {
                this.alergeni = value;
            }
        }

        public string GetAlergeniString
        {
            get
            {
                string alergeniString = "";
                foreach (string a in alergeni)
                    alergeniString += a + " ";
                return alergeniString.Trim();
            }
        }

        public DateTime Datum_Rodjenja
        {
            get
            {
                return datum_rodjenja;
            }
            set
            {
                this.datum_rodjenja = value;
            }
        }

        public Krvne_Grupe Krvna_Grupa
        {
            get
            {
                return krvna_grupa;
            }
            set
            {
                krvna_grupa = value;
            }
        }

        public Pol Pol_Pacijenta
        {
            get
            {
                return pol;
            }
            set
            {
                pol = value;
            }
        }

        public List<string> Hronicne_Bolesti
        {
            get
            {
                return hronicne_bolesti;
            }
            set
            {
                hronicne_bolesti = value;
            }
        } 

        public string Hronicne_Bolesti_String
        {
            get
            {
                string hronicne_bolesti_string = "";
                foreach (string hb in hronicne_bolesti)
                    hronicne_bolesti_string += hb + " ";
                return hronicne_bolesti_string.Trim();
            }
        }

    }
}