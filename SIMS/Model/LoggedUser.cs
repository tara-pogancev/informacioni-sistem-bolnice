// File:    UlogovanKorisnik.cs
// Author:  paracelsus
// Created: Monday, March 22, 2021 6:37:39 PM
// Purpose: Definition of Class UlogovanKorisnik

using Newtonsoft.Json;
using System;

namespace SIMS.Repositories.PatientRepo
{
    public class LoggedUser
    {
        protected String ime;
        protected String prezime;
        protected String jmbg;
        protected String korisnickoIme;
        protected String lozinka;
        protected String email;
        protected String telefon;
        protected bool serijalizuj;

        private Address adresa;

        public LoggedUser(string ime, string prezime, string jmbg, string korisnickoIme, string lozinka, string email, string telefon, Address adresa)
        {
            this.ime = ime;
            this.prezime = prezime;
            this.jmbg = jmbg;
            this.korisnickoIme = korisnickoIme;
            this.lozinka = lozinka;
            this.email = email;
            this.telefon = telefon;
            this.adresa = adresa;
            this.serijalizuj = true;
        }

        public LoggedUser()
        {
            this.serijalizuj = true;
        }

        public String Ime
        {
            get { return ime; }
            set { ime = value; }
        }

        public string Prezime { get => prezime; set => prezime = value; }
        public string Jmbg { get => jmbg; set => jmbg = value; }
        public string KorisnickoIme { get => korisnickoIme; set => korisnickoIme = value; }
        public string Lozinka { get => lozinka; set => lozinka = value; }
        public string Email { get => email; set => email = value; }
        public string Telefon { get => telefon; set => telefon = value; }
        public Address Adresa { get => adresa; set => adresa = value; }

        public bool EqualJmbg(String Jmbg)
        {
            return this.Jmbg == Jmbg;
        }

        [JsonIgnore]
        public String ImePrezime { get => (ime + " " + prezime); }

        [JsonIgnore]
        public String fullAddress
        {
            get
            {
                return this.Adresa.Street + " " + this.Adresa.Number + ", " +
                    this.Adresa.City.Name + " " + this.Adresa.City.PostalCode + ", " + this.Adresa.City.Country.Name;
            }
        }

        [JsonIgnore]
        public String AdresaKorisnika
        {
            get
            {
                return this.Adresa.Street + " " + this.Adresa.Number;
            }
        }

        
        public bool Serijalizuj { get => serijalizuj; set => serijalizuj = value; }

        public bool ShouldSerializePrezime()
        {
            return serijalizuj;
        }

        public bool ShouldSerializeKorisnickoIme()
        {
            return serijalizuj;
        }

        public bool ShouldSerializeLozinka()
        {
            return serijalizuj;
        }

        public bool ShouldSerializeEmail()
        {
            return serijalizuj;
        }

        public bool ShouldSerializeTelefon()
        {
            return serijalizuj;
        }

        public bool ShouldSerializeAdresa()
        {
            return serijalizuj;
        }

        public bool ShouldSerializeIme()
        {
            return serijalizuj;
        }

    }
}