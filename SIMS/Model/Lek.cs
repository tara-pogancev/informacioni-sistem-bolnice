﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Lek
    {

        public string MedicineID { get; set; }
        public string MedicineName { get; set; }
        public List<string> Components { get; set; }
        public MedicineApprovalStatus ApprovalStatus { get; set; }
        public String IDSubstitution { get; set; }

        public Lek()
        {
            ApprovalStatus = MedicineApprovalStatus.Waiting;
        }

        public Lek(string iD, string naziv, List<string> components, String idSubstitution)
        {
            MedicineID = iD;
            MedicineName = naziv;
            Components = components;
            ApprovalStatus = MedicineApprovalStatus.Waiting;
            IDSubstitution = idSubstitution;
        }

        public String getComponentsList()
        {
            string componentsString = "";
            if (Components.Count == 0 || Components.Contains(""))
                return "Nije navedeno";

            foreach (string a in Components)
                componentsString += AlergeniStorage.Instance.Read(a).Naziv + ", ";
            return componentsString.Remove(componentsString.Length - 2);
        }

        [JsonIgnore]
        public String ApprovalStatusString
        {
            get
            {
                if (ApprovalStatus == MedicineApprovalStatus.Waiting)
                    return "Na čekanju";
                else if (ApprovalStatus == MedicineApprovalStatus.Denied)
                    return "Odbijen";
                else return "Prihvaćen";
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
