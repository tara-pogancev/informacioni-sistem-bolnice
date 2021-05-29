using System;
using System.Collections.Generic;
using System.Text;
using SIMS.DTO;
using SIMS.Model;
using SIMS.Repositories.ReceiptRepo;
using SIMS.Repositories.SecretaryRepo;

namespace SIMS.Service
{
    public class ReceiptService
    {
        private IReceiptRepository receiptRepository = new ReceiptFileRepository();

        public ReceiptService()
        {

        }

        public List<Receipt> GetAllReceipts() => receiptRepository.GetAll();

        public void UpdateReceipt(Receipt receipt) => receiptRepository.Update(receipt);

        public void DeleteReceipt(String key) => receiptRepository.Delete(key);

        public void SaveReceipt(Receipt receipt) => receiptRepository.Save(receipt);

        public Receipt GetReceipt(String key) => receiptRepository.FindById(key);

        public List<Receipt> ReadByPatient(Patient patient)
        {
            return receiptRepository.ReadByPatient(patient);
        }

        public ReceiptDTO GetDTO(Receipt receipt)
        {
            return new ReceiptDTO(receipt);
        }

        public List<ReceiptDTO> GetDTOFromList(List<Receipt> list)
        {
            List<ReceiptDTO> retVal = new List<ReceiptDTO>();
            foreach (Receipt receipt in list)
                retVal.Add(GetDTO(receipt));

            return retVal;
        }
    }
}
