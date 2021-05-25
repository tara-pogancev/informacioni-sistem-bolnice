using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Model;
using SIMS.Service;

namespace SIMS.Controller
{
    public class LastLoginController
    {
        private LastLoginService lastLoginService = new LastLoginService();

        public LastLoginController()
        {

        }

        public bool CheckForLastLogged()
        {
            return lastLoginService.CheckForLastLogged();
        }

        public void ClearAll()
        {
            lastLoginService.ClearAll();
        }

        public LoggedUser GetLastLogged()
        {
            return lastLoginService.GetLastLogged();
        }

        public void SaveLoggedUser(LoggedUser user)
        {
            lastLoginService.SaveLoggedUser(user);
        }

        public Boolean IsSelfLastLogged(LoggedUser user)
        {
            return lastLoginService.IsSelfLastLogged(user);
        }

    }
}
