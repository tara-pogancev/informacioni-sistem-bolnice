// File:    Lekar.cs
// Author:  paracelsus
// Created: Monday, March 22, 2021 7:32:13 PM
// Purpose: Definition of Class Lekar

using System;
using System.Collections.Generic;

namespace Model
{
    public class Lekar : UlogovanKorisnik
    {
        private int daniGodisnjegOdmora;

        public Lekar()
        {
        }

        public Lekar(string ime, string prezime, string jmbg, string korisnickoIme, string lozinka, string email, string telefon, Adresa adresa, int daniGodisnjegOdmora) : base(ime, prezime, jmbg, korisnickoIme, lozinka, email, telefon, adresa)
        {
            this.daniGodisnjegOdmora = daniGodisnjegOdmora;
        }

        public int DaniGodisnjegOdmora { get => daniGodisnjegOdmora; set => daniGodisnjegOdmora = value; }

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

    }
}