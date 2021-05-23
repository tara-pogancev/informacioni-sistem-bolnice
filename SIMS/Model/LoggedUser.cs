// File:    UlogovanKorisnik.cs
// Author:  paracelsus
// Created: Monday, March 22, 2021 6:37:39 PM
// Purpose: Definition of Class UlogovanKorisnik

using Newtonsoft.Json;
using System;

namespace SIMS.Repositories.SecretaryRepo
{
    public class LoggedUser
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Jmbg { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Address Address { get; set; }
        public bool Serialize { get; set; }

        public LoggedUser(string name, string lastName, string jmbg, string username, string password, string email, string phone, Address address)
        {
            Name = name;
            LastName = lastName;
            Jmbg = jmbg;
            Username = username;
            Password = password;
            Email = email;
            Phone = phone;
            Address = address;
            Serialize = true;
        }

        public LoggedUser()
        {
            Serialize = true;
        }

        public bool EqualJmbg(String Jmbg)
        {
            return this.Jmbg == Jmbg;
        }

        [JsonIgnore]
        public String FullName { get => (Name + " " + LastName); }

        [JsonIgnore]
        public String FullAddressString
        {
            get
            {
                return "Novi Sad, Despota Stefana 7";
                //return this.Address.Street + " " + this.Address.Number + ", " + this.Address.City.Name;
            }
        }

        [JsonIgnore]
        public String AddressString
        {
            get
            {
                return this.Address.Street + " " + this.Address.Number;
            }
        }

        public bool ShouldSerializeName()
        {
            return Serialize;
        }

        public bool ShouldSerializeLastName()
        {
            return Serialize;
        }

        public bool ShouldSerializeUsername()
        {
            return Serialize;
        }

        public bool ShouldSerializePassword()
        {
            return Serialize;
        }

        public bool ShouldSerializeEmail()
        {
            return Serialize;
        }

        public bool ShouldSerializePhone()
        {
            return Serialize;
        }

        public bool ShouldSerializeAddress()
        {
            return Serialize;
        }

    }
}