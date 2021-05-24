using Newtonsoft.Json;
using SIMS.Repositories.AllergenRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Model
{
    public class Medication
    {

        public string MedicineID { get; set; }
        public string MedicineName { get; set; }
        public List<string> Components { get; set; }
        public string IDSubstitution { get; set; }

        public MedicineApprovalStatus ApprovalStatus { get; set; }

        public Medication()
        {
            Components = new List<string>();
            ApprovalStatus = MedicineApprovalStatus.Waiting;
        }

        public Medication(string id, string name, List<string> components, string iDSubstitution)
        {
            MedicineID = id;
            MedicineName = name;
            Components = components;
            ApprovalStatus = MedicineApprovalStatus.Waiting;
            IDSubstitution = iDSubstitution; 
        }

        public String GetComponentsList()
        {
            string componentsString = "";
            if (Components.Count == 0 || Components.Contains(""))
                return "Nije navedeno";

            foreach (string a in Components)
                componentsString += AllergenFileRepository.Instance.FindById(a).Name + ", ";
            return componentsString.Remove(componentsString.Length - 2);
        }

        [JsonIgnore]
        public String ApprovalStatusString
        {
            get
            {
                if (ApprovalStatus == MedicineApprovalStatus.Accepted)
                    return "Prihvaćen";
                else if (ApprovalStatus == MedicineApprovalStatus.Waiting)
                    return "Na čekanju";
                else return "Odbijen";

            }
        }
    }

    public enum MedicineApprovalStatus
    {
        Accepted = 0,
        Denied, 
        Waiting
    }
    
}
