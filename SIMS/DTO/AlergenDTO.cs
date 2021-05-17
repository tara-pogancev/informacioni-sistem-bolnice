using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace SIMS.DTO
{
    public class AlergenDTO
    {
        public string AlergenID { get; set; }
        public string AlergenNaziv { get; set; }
        public Boolean IsIncludedInMedicine { get; set; }

        public AlergenDTO()
        {

        }

        public AlergenDTO(Allergen alergen, Medication medicine)
        {
            AlergenID = alergen.ID;
            AlergenNaziv = alergen.Name;
            IsIncludedInMedicine = medicine.Components.Contains(AlergenID);
        }

        public List<AlergenDTO> GetAllAlergenList(Medication medicine)
        {
            List<AlergenDTO> retVal = new List<AlergenDTO>();

            foreach (Allergen currentAlergen in AllergenRepository.Instance.ReadList())
                retVal.Add(new AlergenDTO(currentAlergen, medicine));

            return retVal;
        }

    }
}
