using SIMS.Model;
using SIMS.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Controller
{
    public class ManagerController
    {
        private ManagerService managerService = new ManagerService();

        public ManagerController()
        {

        }

        public List<Manager> GetAllManagers()
        {
            return managerService.GetAllManagers();
        }
    }
}
