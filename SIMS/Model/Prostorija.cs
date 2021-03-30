// File:    Prostorija.cs
// Author:  paracelsus
// Created: Monday, March 22, 2021 6:41:35 PM
// Purpose: Definition of Class Prostorija

using System;
using System.Collections.Generic;

namespace Model
{
    public class Prostorija
    {
        private Adresa adresa;
        private int sprat;
        private int broj;
        private bool dostupna;
        private TipProstorije tipProstorije;
        private Dictionary<string, int> kolicineDinamickihOprema = new Dictionary<string, int>();
        private Dictionary<string, int> kolicineStatickihOprema = new Dictionary<string, int>();

        public Prostorija()
        {
        }

        public Prostorija(Adresa adresa, int sprat, int broj, bool dostupna, TipProstorije tipProstorije)
        {
            this.adresa = adresa;
            this.sprat = sprat;
            this.broj = broj;
            this.dostupna = dostupna;
            this.tipProstorije = tipProstorije;
        }


        public Prostorija(Grad grad, string ulica, int brojUUlici, int sprat, int brojProstorije, bool dostupna, TipProstorije tipProstorije)
        {
            this.adresa = new Adresa(ulica, brojUUlici, grad);
            this.sprat = sprat;
            this.broj = brojProstorije;
            this.dostupna = dostupna;
            this.tipProstorije = tipProstorije;
        }

        public void SetKolicinaDinamickeOpreme(string id, int kolicina)
        {
            kolicineDinamickihOprema[id] = kolicina;
        }

        public void SetKolicinaStatickeOpreme(string id, int kolicina)
        { 
            kolicineStatickihOprema[id] = kolicina;
        }

        public int GetKolicinaDinamickeOpreme(string id)
        {
            int retVal;

            if (!kolicineDinamickihOprema.TryGetValue(id, out retVal))
            {
                return 0;
            }

            return retVal;
        }

        public int GetKolicinaStatickeOpreme(string id)
        {
            int retVal;

            if (!kolicineStatickihOprema.TryGetValue(id, out retVal))
            {
                return 0;
            }

            return retVal;
        }

        public Adresa Adresa { get => adresa; set => adresa = value; }
        public int Sprat { get => sprat; set => sprat = value; }
        public int Broj { get => broj; set => broj = value; }
        public bool Dostupna { get => dostupna; set => dostupna = value; }
        public TipProstorije TipProstorije { get => tipProstorije; set => tipProstorije = value; }

        public String BrojString { get => broj.ToString(); }
    }
}