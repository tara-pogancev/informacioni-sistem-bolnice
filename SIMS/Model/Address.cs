using System;

namespace SIMS.Model
{
   public class Address
   {
        public City City { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }

        public Address()
        {
            City = new City();
        }

        public Address(string street, string number, City city)
        {
            City = city;
            Street = street;
            Number = number;
        }

        public override string ToString()
        {
            return Street + " " + Number;
        }
    }
}