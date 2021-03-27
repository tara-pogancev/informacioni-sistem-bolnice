// File:    Sala.cs
// Author:  paracelsus
// Created: 22 March 2021 18:40:47
// Purpose: Definition of Class Sala

using System;

namespace P1
{
    public class Sala : Prostorija
    {
        public Sala(String naziv, Adresa adresa, int sprat, int broj, bool dostupnost) : base(naziv, adresa, sprat, broj, dostupnost)
        {
        }
    }
}