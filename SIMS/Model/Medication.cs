using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
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

        public Medication(string iD, string naziv, List<string> components, string iDSubstitution)
        {
            MedicineID = iD;
            MedicineName = naziv;
            Components = components;
            ApprovalStatus = MedicineApprovalStatus.Waiting;
            IDSubstitution = iDSubstitution; 
        }

        public String getComponentsList()
        {
            string componentsString = "";
            if (Components.Count == 0 || Components.Contains(""))
                return "Nije navedeno";

            foreach (string a in Components)
                componentsString += AllergenRepository.Instance.Read(a).Name + ", ";
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
