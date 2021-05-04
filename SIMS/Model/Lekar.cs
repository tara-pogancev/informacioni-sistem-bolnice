// File:    Lekar.cs
// Author:  paracelsus
// Created: Monday, March 22, 2021 7:32:13 PM
// Purpose: Definition of Class Lekar

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Model
{
    public class Lekar : UlogovanKorisnik
    {
        public int DaniGodisnjegOdmora { get; set; }

        public Specijalizacija SpecijalizacijaLekara { get; set; }

        public Lekar()
        {
        }

        public Lekar(string ime, string prezime, string jmbg, string korisnickoIme, string lozinka, string email, string telefon, Adresa adresa, Specijalizacija specijalizacija, int daniGodisnjegOdmora) : base(ime, prezime, jmbg, korisnickoIme, lozinka, email, telefon, adresa)
        {
            DaniGodisnjegOdmora = daniGodisnjegOdmora;
            SpecijalizacijaLekara = specijalizacija;
        }
             
        public List<Termin> getZauzetiTermini()
        {
            List<Termin> retVal = new List<Termin>();

            retVal = TerminStorage.Instance.ReadByDoctor(this);

            return retVal;

        }

        // Salje informacije o novom terminu
        public Boolean IsFree(Termin terminNew)
        {
            foreach (Termin t in TerminStorage.Instance.ReadByDoctor(this))
            {
                if (terminNew.KrajnjeVreme > t.PocetnoVreme && terminNew.KrajnjeVreme <= t.KrajnjeVreme)
                    return false;

                if (terminNew.PocetnoVreme >= t.PocetnoVreme && terminNew.PocetnoVreme < t.KrajnjeVreme)
                    return false;
            }

            return true;
        }

        // Salje izmenjen termin ali njega ignorise prilikom provere
        public Boolean IsFreeUpdate(Termin terminNew)
        {
            foreach (Termin t in TerminStorage.Instance.ReadByDoctor(this))
            {
                if (t.TerminKey != terminNew.TerminKey)
                {
                    if (terminNew.KrajnjeVreme > t.PocetnoVreme && terminNew.KrajnjeVreme <= t.KrajnjeVreme)
                        return false;

                    if (terminNew.PocetnoVreme >= t.PocetnoVreme && terminNew.PocetnoVreme < t.KrajnjeVreme)
                        return false;
                }
            }

            return true;
        }

        public bool ShouldSerializeDaniGodisnjegOdmora()
        {
            return serijalizuj;
        }

        [JsonIgnore]
        public String NameAndSpecialization { get { return ImePrezime + ", " + Specialization; } }

        [JsonIgnore]
        public String Specialization 
        { 
            get 
            {
                if (SpecijalizacijaLekara == Specijalizacija.OpstaPraksa)
                    return "Lekar opšte prakse";
                else if (SpecijalizacijaLekara == Specijalizacija.Hirurg)
                    return "Hirurg";
                else if (SpecijalizacijaLekara == Specijalizacija.Internista)
                    return "Internista";
                else if (SpecijalizacijaLekara == Specijalizacija.Dermatolog)
                    return "Dermatolog";
                else if (SpecijalizacijaLekara == Specijalizacija.Kardiolog)
                    return "Kardiolog";
                else if (SpecijalizacijaLekara == Specijalizacija.Otorinolaringolog)
                    return "Otorinolaringolog";
                else if (SpecijalizacijaLekara == Specijalizacija.Stomatolog)
                    return "Stomatolog";
                else if (SpecijalizacijaLekara == Specijalizacija.Urolog)
                    return "Urolog";
                else if (SpecijalizacijaLekara == Specijalizacija.Ginekolog)
                    return "Ginekolog";
                else
                    return "Neurolog";
            } 

        }


    }
}