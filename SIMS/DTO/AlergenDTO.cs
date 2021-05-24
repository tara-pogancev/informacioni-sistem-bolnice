using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Repositories.SecretaryRepo;
using SIMS.Repositories.AllergenRepo;
using SIMS.Model;

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

            foreach (Allergen currentAlergen in AllergenFileRepository.Instance.GetAll())
                retVal.Add(new AlergenDTO(currentAlergen, medicine));

            return retVal;
        }

    }
}
