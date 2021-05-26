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
        public List<Allergen> Components { get; set; }
        public string IDSubstitution { get; set; }

        public MedicineApprovalStatus ApprovalStatus { get; set; }

        public Medication()
        {
            Components = new List<Allergen>();
            ApprovalStatus = MedicineApprovalStatus.Waiting;
        }

        public Medication(string id, string name, List<Allergen> components, string iDSubstitution)
        {
            MedicineID = id;
            MedicineName = name;
            Components = components;

            foreach (var allergen in components)
            {
                allergen.Name = AllergenFileRepository.Instance.FindById(allergen.ID).Name;
            }

            ApprovalStatus = MedicineApprovalStatus.Waiting;
            IDSubstitution = iDSubstitution; 
        }

        public String GetComponentsList()
        {
            string componentsString = "";
            if (Components.Count == 0)
                return "Nije navedeno";

            foreach (var a in Components)
                componentsString += a.Name;
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
