using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Expression.Interactivity.Media;
using Microsoft.Internal.VisualStudio.Shell;

namespace SIMS.Model
{
    public class ApplicationFeedback
    {
        public DateTime SubmissionDate { get; set; }
        public String Comment { get; set; }
        public LoggedUser User { get; set; }
        public int Grade { get; set; }
        public String FeedbackID { get; set; }
       

        public ApplicationFeedback() 
        {
        }

        public ApplicationFeedback(String comment, LoggedUser loggedUser, int grade)
        {
            this.SubmissionDate=DateTime.Now;
            this.FeedbackID = loggedUser.Jmbg+this.SubmissionDate.ToString("HH:mm:ss");
            this.Comment = comment;
            this.User = loggedUser;
            this.Grade = grade;
           
        }

        
    }
}
