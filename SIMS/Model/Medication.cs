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
        public List<Component> Components { get; set; }
        public string IDSubstitution { get; set; }

        public MedicineApprovalStatus ApprovalStatus { get; set; }

        public Medication()
        {
            Components = new List<Component>();
            ApprovalStatus = MedicineApprovalStatus.Waiting;
        }

        public Medication(string id, string name, List<Component> components, string iDSubstitution)
        {
            MedicineID = id;
            MedicineName = name;
            Components = components;

            foreach (var allergen in components)
            {
                allergen.Name = ComponentFileRepository.Instance.FindById(allergen.ID).Name;
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
                componentsString += a.Name + ", ";
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

        public Boolean IncludesAllergen(Component allergen) 
        {
            foreach(Component currentAllergen in Components)
            {
                if (currentAllergen.ID == allergen.ID)
                    return true;
            }

            return false;
        }

        public void RemoveComponent(Component allergen)
        {
            for (int i = 0; i < Components.Count; i++)
            {
                Component currentComponent = Components[i];
                if (currentComponent.ID == allergen.ID)
                {
                    Components.Remove(currentComponent);
                    i--;
                }
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
