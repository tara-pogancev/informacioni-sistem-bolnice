// File:    Grad.cs
// Author:  paracelsus
// Created: Monday, March 22, 2021 8:19:13 PM
// Purpose: Definition of Class Grad

using System;

namespace SIMS.Model
{
    public class City
    {

        public City()
        {
        }

        public City(string name, int postalCode, Country country)
        {
            Name = name;
            PostalCode = postalCode;
            Country = country;
        }

        public string Name { get; set; }
        public int PostalCode { get; set; }
        public Country Country { get; set; }

    }

}
