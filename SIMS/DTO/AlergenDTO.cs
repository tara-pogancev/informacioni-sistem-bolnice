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

        public AlergenDTO(Alergen alergen, Lek medicine)
        {
            AlergenID = alergen.ID;
            AlergenNaziv = alergen.Naziv;
            IsIncludedInMedicine = medicine.Components.Contains(AlergenID);
        }

        public List<AlergenDTO> GetAllAlergenList(Lek medicine)
        {
            List<AlergenDTO> retVal = new List<AlergenDTO>();

            foreach (Alergen currentAlergen in AlergeniStorage.Instance.ReadList())
                retVal.Add(new AlergenDTO(currentAlergen, medicine));

            return retVal;
        }

    }
}
