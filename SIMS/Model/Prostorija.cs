// File:    Prostorija.cs
// Author:  paracelsus
// Created: Monday, March 22, 2021 6:41:35 PM
// Purpose: Definition of Class Prostorija

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Model
{
    public class Prostorija
    {
        private string broj;
        private bool dostupna;
        private TipProstorije tipProstorije;
        private bool serijalizuj;
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
            serijalizuj = true;
        }

        public Prostorija(string broj, bool dostupna, TipProstorije tipProstorije)
        {
            kolicineOpreme = new Dictionary<string, int>();
            this.broj = broj;
            this.dostupna = dostupna;
            this.tipProstorije = tipProstorije;
            serijalizuj = true;
        }

        public Prostorija(string broj, bool dostupna, TipProstorije tipProstorije, Dictionary<string, int> kolicineOpreme)
        {
            kolicineOpreme = new Dictionary<string, int>();
            this.broj = broj;
            this.dostupna = dostupna;
            this.tipProstorije = tipProstorije;
            this.kolicineOpreme = kolicineOpreme;
            serijalizuj = true;
        }


        [JsonIgnore]
        public string DostupnaToString
        {
            get
            {
                return Conversion.DostupnostProstorijeToString(dostupna);
            }
        }

        [JsonIgnore]
        public string TipProstorijeToString
        {
            get
            {
                return Conversion.TipProstorijeToString(tipProstorije);
            }
        }

        public TipProstorije TipProstorije { get => tipProstorije; set => tipProstorije = value; }
        public string Broj { get => broj; set => broj = value; }
        public bool Dostupna { get => dostupna; set => dostupna = value; }
        [JsonIgnore]
        public bool Serijalizuj { get => serijalizuj; set => serijalizuj = value; }

        public bool ShouldSerializeDostupna()
        {
            return serijalizuj;
        }
       
        public bool ShouldSerializeTipProstorije()
        {
            return serijalizuj;
        }

        public bool ShouldSerializekolicineOpreme()
        {
            return serijalizuj;
        }
        
    }
}