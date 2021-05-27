using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Model
{
    public class Survey
    {
        protected String surveyID;
        protected String comment;
        protected DateTime submissionDate;
        protected String ownerID;

        public string SurveyID { get => surveyID; set => surveyID = value; }
        
        public string Comment { get => comment; set => comment = value; }
        public DateTime SubmissionDate { get => submissionDate; set => submissionDate = value; }
        public string IdVlasnika { get => ownerID; set => ownerID = value; }

        public Survey() {
            submissionDate = DateTime.Now;
        }

        public Survey(String komentar,String idVlasnika)
        {
           
            this.comment = komentar;
            this.ownerID = idVlasnika;
            submissionDate = DateTime.Now;

        }

       
    }
}
