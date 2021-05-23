// File:    UpravnikStorage.cs
// Author:  paracelsus
// Created: Thursday, March 25, 2021 4:23:30 PM
// Purpose: Definition of Class UpravnikStorage

using SIMS.Repositories.ManagerRepo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace SIMS.Repositories.SecretaryRepo
{
    public class ManagerFileRepository : GenericFileRepository<string, Manager, ManagerFileRepository>,IManagerRepository
    {
        protected override string getPath()
        {
            return @".\..\..\..\Data\upravnici.json";
        }
        protected override string getKey(Manager entity)
        {
            return entity.Jmbg;
        }
        protected override void RemoveReferences(string key)
        {

        }

        public Manager ReadUser(String user)
        {
            foreach (Manager u in this.GetAll())
            {
                if (u.Username == user)
                    return u;
            }

            return null;
        }
    }
}