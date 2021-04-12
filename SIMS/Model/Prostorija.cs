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
        private string broj;
        private bool dostupna;
        private TipProstorije tipProstorije;
        public Dictionary<string, int> kolicineOpreme
        {
            get; set;
        }


        public Prostorija()
        {
            kolicineOpreme = new Dictionary<string, int>();
            this.broj = "";
            this.dostupna = false;
            this.tipProstorije = TipProstorije.bolesnicka;
        }

        public Prostorija(string broj, bool dostupna, TipProstorije tipProstorije)
        {
            kolicineOpreme = new Dictionary<string, int>();
            this.broj = broj;
            this.dostupna = dostupna;
            this.tipProstorije = tipProstorije;
        }

        public Prostorija(string broj, bool dostupna, TipProstorije tipProstorije, Dictionary<string, int> kolicineOpreme)
        {
            kolicineOpreme = new Dictionary<string, int>();
            this.broj = broj;
            this.dostupna = dostupna;
            this.tipProstorije = tipProstorije;
            this.kolicineOpreme = kolicineOpreme;
        }

        public void SetKolicinaOpreme(string id, int kolicina)
        {
            kolicineOpreme[id] = kolicina;
        }

        public int GetKolicinaOpreme(string id)
        {
            int retVal;

            if (!kolicineOpreme.TryGetValue(id, out retVal))
            {
                return 0;
            }

            return retVal;
        }

        public string Broj { get => broj; set => broj = value; }
        public bool Dostupna { get => dostupna; set => dostupna = value; }

        public string DostupnaToString
        {
            get
            {
                return Conversion.DostupnostProstorijeToString(dostupna);
            }
        }

        public string TipProstorijeToString
        {
            get
            {
                return Conversion.TipProstorijeToString(tipProstorije);
            }
        }

        public TipProstorije TipProstorije { get => tipProstorije; set => tipProstorije = value; }
    }
}