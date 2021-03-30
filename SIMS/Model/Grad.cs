// File:    Grad.cs
// Author:  paracelsus
// Created: Monday, March 22, 2021 8:19:13 PM
// Purpose: Definition of Class Grad

using System;

namespace Model
{
    public class Grad
    {
        private String naziv;
        private int postanskiBroj;
        private Drzava drzava;

        public Grad()
        {
        }

        public Grad(string naziv, int postanskiBroj, Drzava drzava)
        {
            this.naziv = naziv;
            this.postanskiBroj = postanskiBroj;
            this.drzava = drzava;
        }

        public string Naziv { get => naziv; set => naziv = value; }
        public int PostanskiBroj { get => postanskiBroj; set => postanskiBroj = value; }

    }


}
