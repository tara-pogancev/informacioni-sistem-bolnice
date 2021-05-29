using SIMS.Model;
using SIMS.Repositories.ManagerRepo;
using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Service
{
    public class ManagerService
    {
        private IManagerRepository managerRepostitory = new ManagerFileRepository();

        public ManagerService()
        {

        }

        public List<Manager> GetAllManagers()
        {
            return managerRepostitory.GetAll();
        }

    }
}
