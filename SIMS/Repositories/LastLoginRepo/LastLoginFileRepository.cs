using SIMS.Model;
using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Repositories.LastLoginRepo
{
    public class LastLoginFileRepository : GenericFileRepository<String, LoggedUser, LastLoginFileRepository>, ILastLoginRepository
    {
        public bool CheckForLastLogged()
        {
            return (!(GetAll().Count == 0));
        }

        public void ClearAll()
        {
            foreach (LoggedUser user in GetAll())
                Delete(user.Jmbg);
        }

        public LoggedUser GetLastLogged()
        {
            return GetAll()[0];
        }

        protected override string getKey(LoggedUser entity)
        {
            return entity.Jmbg;
        }

        protected override string getPath()
        {
            return @".\..\..\..\Data\lastLogin.json";
        }

        protected override void RemoveReferences(string key)
        {
        }

        protected override void ShouldSerialize(LoggedUser entity)
        {
            entity.Serialize = true;
        }
    }
}
