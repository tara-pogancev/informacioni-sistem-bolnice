// File:    Sekretar.cs
// Author:  paracelsus
// Created: Monday, March 22, 2021 6:35:51 PM
// Purpose: Definition of Class Sekretar

using System;

namespace SIMS.Model
{ 
   public class Secretary : LoggedUser
   {
        public int VacationDays { get; set; }
        public string Theme { get; set; }
        public string Language { get; set; }

        public Secretary() :base()
        {
        }

        public Secretary(string name, string lastName, string jmbg, string username, string password, string email, string phone, Address address, int vacationDays) : base(name, lastName, jmbg, username, password, email, phone, address)
        {
            VacationDays = vacationDays;
            Theme = "Dark";
            Language = "SR";
        }

        public Secretary(Secretary s) : base(s.Name, s.LastName, s.Jmbg, s.Username, s.Password, s.Email, s.Phone, s.Address)
        {
            VacationDays = s.VacationDays;
            Theme = "Dark";
            Language = "SR";
        }

        public bool ShouldSerializeDaniGodisnjegOdmora()
        {
            return Serialize;
        }
    }
   
}