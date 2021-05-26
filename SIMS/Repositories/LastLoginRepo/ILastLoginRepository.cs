using SIMS.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Repositories.LastLoginRepo
{
    interface ILastLoginRepository : IGenericRepository<LoggedUser, String>
    {
        public Boolean CheckForLastLogged();
        public LoggedUser GetLastLogged();
        public void ClearAll();
    }
}
