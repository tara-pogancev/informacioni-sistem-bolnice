using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    class ObavestenjeUloga
    {
        public string Uloga { get; set; }
        public int Indeks { get; set; }
        public string Key { get; set; }

        public ObavestenjeUloga(string uloga, int indeks)
        {
            Uloga = uloga;
            Indeks = indeks;
        }

        public ObavestenjeUloga(string uloga, int indeks, string key)
        {
            Uloga = uloga;
            Indeks = indeks;
            Key = key;
        }
    }
}
