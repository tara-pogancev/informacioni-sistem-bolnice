using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Repositories.SecretaryRepo
{
    class NotificationRole
    {
        public string Uloga { get; set; }
        public int Indeks { get; set; }
        public string Key { get; set; }

        public NotificationRole(string uloga, int indeks)
        {
            Uloga = uloga;
            Indeks = indeks;
        }

        public NotificationRole(string uloga, int indeks, string key)
        {
            Uloga = uloga;
            Indeks = indeks;
            Key = key;
        }
    }
}
