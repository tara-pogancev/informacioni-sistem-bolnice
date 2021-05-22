

using System;

namespace SIMS.Repositories.SecretaryRepo
{
   public class Country
   {

        public Country()
        {
        }

        public Country(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

    }
}