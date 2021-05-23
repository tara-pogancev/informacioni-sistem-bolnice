// File:    Upravnik.cs
// Author:  paracelsus
// Created: Monday, March 22, 2021 6:35:50 PM
// Purpose: Definition of Class Upravnik

using System;

namespace SIMS.Repositories.SecretaryRepo
{
   public class Manager : LoggedUser
   {
        public int VacationDays { get; set; }

        public Manager()
        {
        }

        public Manager(string name, string lastName, string jmbg, string username, string password, string email, string phone, Address address, int vacationDays) : base(name, lastName, jmbg, username, password, email, phone, address)
        {
            VacationDays = vacationDays;
        }        

        public bool ShouldSerializeDaniGodisnjegOdmora()
        {
            return Serialize;
        }
    }

}