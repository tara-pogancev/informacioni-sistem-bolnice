using SIMS.Model;
using SIMS.Repositories.LastLoginRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Service
{
    public class LastLoginService
    {
        private ILastLoginRepository lastLoginRepository = new LastLoginFileRepository();

        public LastLoginService()
        {

        }

        public bool CheckForLastLogged()
        {
            return lastLoginRepository.CheckForLastLogged();
        }

        public void ClearAll()
        {
            lastLoginRepository.ClearAll();
        }

        public LoggedUser GetLastLogged()
        {
            return lastLoginRepository.GetLastLogged();
        }

        public void SaveLoggedUser(LoggedUser user)
        {
            lastLoginRepository.ClearAll();
            lastLoginRepository.Save(user);
        }

        public Boolean IsSelfLastLogged(LoggedUser user)
        {
            if (CheckForLastLogged())
                if (user.Jmbg == GetLastLogged().Jmbg)
                    return true;

            return false;
        }

    }
}
