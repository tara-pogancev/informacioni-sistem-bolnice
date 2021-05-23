// File:    Pacijent.cs
// Author:  paracelsus
// Created: Monday, March 22, 2021 6:35:53 PM
// Purpose: Definition of Class Pacijent

using Newtonsoft.Json;
using SIMS.Model;
using System;
using System.Collections.Generic;

namespace SIMS.Repositories.SecretaryRepo
{
    public class Patient : LoggedUser
    {
        private String lbo;
        private bool gost;
        private List<string> alergeni;
        private DateTime datum_rodjenja;
        private BloodType krvna_grupa;
        private SexType pol;
        private bool banovanKorisnik;
        private List<string> hronicne_bolesti = new List<string>();

        public Patient(string ime, string prezime, string jmbg, string korisnickoIme, string lozinka, string email, string telefon, Address adresa, String lbo, Boolean gost, List<string> alergeni) : base(ime, prezime, jmbg, korisnickoIme, lozinka, email, telefon, adresa)
        {
            this.lbo = lbo;
            this.gost = gost;
            this.alergeni = alergeni;
            this.banovanKorisnik = false;
            this.serijalizuj = true;
        }

        public Patient(string ime, string prezime, string jmbg, string korisnickoIme, string lozinka, string email, string telefon, Address adresa, String lbo, Boolean gost, List<string> alergeni, DateTime datum_rodjenja, BloodType krvna_grupa, SexType pol, List<string> hronicne_bolesti) : base(ime, prezime, jmbg, korisnickoIme, lozinka, email, telefon, adresa)
        {
            this.lbo = lbo;
            this.gost = gost;
            this.alergeni = alergeni;
            this.datum_rodjenja = datum_rodjenja;
            this.krvna_grupa = krvna_grupa;
            this.pol = pol;
            this.hronicne_bolesti = hronicne_bolesti;
            this.banovanKorisnik = false;
            this.Serijalizuj = true;
        }

        public Patient() : base()
        {
            this.banovanKorisnik = false;
            this.serijalizuj = true;
        }

        public Patient(string ime, string prezime, string jmbg) : base(ime, prezime, jmbg, "", "", "", "", new Address("", "", new City("", 0, new Country(""))))
        {
            this.lbo = "";
            this.gost = true;
            this.alergeni = new List<string>();
            this.datum_rodjenja = DateTime.MinValue;
            this.krvna_grupa = BloodType.Op;
            this.pol = SexType.Muški;
            this.hronicne_bolesti = new List<string>();
        }

        [JsonIgnore]
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

        public void SetAttributes(Patient p)
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


        [JsonIgnore]
        public string GetAlergeniString
        {
            get
            {
                /*string alergeniString = "";
                if (alergeni.Count == 0 || alergeni.Contains(""))
                    return "Nema";

                foreach (string a in alergeni)
                    alergeniString += AlergeniStorage.Instance.ReadEntity(a).Naziv + ", ";
                return alergeniString.Remove(alergeniString.Length - 2); */
                return "Nema";
            }
        }

        public bool Available(Appointment appointment)
        {
            return appointment.Pacijent.Jmbg == this.Jmbg;

        }

        [JsonIgnore]
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

        [JsonIgnore]

        public String DatumString { get => datum_rodjenja.ToString("dd.MM.yyyy."); }
        [JsonIgnore]

        public String PolString
        {
            get

            {
                if (pol == SexType.Muški)
                    return "Muško";
                else
                    return "Žensko";
            }
        }

        [JsonIgnore]
        public String KrvnaGrupaString
        {
            get
            {
                if (krvna_grupa == BloodType.ABn)
                    return "AB-";
                else if (krvna_grupa == BloodType.ABp)
                    return "AB+";
                else if (krvna_grupa == BloodType.Ap)
                    return "A+";
                else if (krvna_grupa == BloodType.An)
                    return "A-";
                else if (krvna_grupa == BloodType.Bp)
                    return "B+";
                else if (krvna_grupa == BloodType.Bn)
                    return "B-";
                else if (krvna_grupa == BloodType.Op)
                    return "O+";
                else if (krvna_grupa == BloodType.On)
                    return "O-";

                return null;
            }
        }

        public bool IsAlergic(Medication lek)
        {
            foreach (string a in this.Alergeni)
            {
                if (lek.Components.Contains(a))
                    return true;
            }
            return false;
        }

        public string Lbo { get => lbo; set => lbo = value; }
        public bool Gost { get => gost; set => gost = value; }
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

        public string GetHronicneBolestiString
        {
            get
            {
                string hronBolestiString = "";
                if (hronicne_bolesti.Count == 0 || hronicne_bolesti.Contains(""))
                    return "Nema";

                foreach (string a in hronicne_bolesti)
                    hronBolestiString += a + ", ";
                return hronBolestiString.Remove(hronBolestiString.Length - 2);
            }
        }

        public SexType Pol_Pacijenta
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

        public BloodType Krvna_Grupa
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
        
        public bool BanovanKorisnik { get => banovanKorisnik; set => banovanKorisnik = value; }

        public bool ShouldSerializeKrvna_Grupa()
        {
            return serijalizuj;
        }
        public bool ShouldSerializePol_Pacijenta()
        {
            return serijalizuj;
        }
        public bool ShouldSerializeGetHronicneBolestiString()
        {
            return serijalizuj;
        }
        public bool ShouldSerializeDatum_Rodjenja()
        {
            return serijalizuj;
        }
        public bool ShouldSerializeAlergeni()
        {
            return serijalizuj;
        }
        public bool ShouldSerializeGost()
        {
            return serijalizuj;
        }
        public bool ShouldSerializeLbo()
        {
            return serijalizuj;
        }
        public bool ShouldSerializeBanovanKorisnik()
        {
            return serijalizuj;
        }

        
    }
}