using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Model;

namespace SIMS.Repositories.FeedbackRepo
{
    class ApplicationFeedbackFileRepository : GenericFileRepository<string, ApplicationFeedback, ApplicationFeedbackFileRepository>,IApplicationFeedbackRepository
    {
        protected override string getKey(ApplicationFeedback entity)
        {
            return entity.FeedbackID;
        }

        protected override string getPath()
        {
            return @".\..\..\..\Data\feedbackAplikacije.json";
        }

        protected override void RemoveReferences(string key)
        {
            throw new NotImplementedException();
        }

        protected override void ShouldSerialize(ApplicationFeedback entity)
        {
            entity.User.Serialize = false;
            
        }
    }
}
