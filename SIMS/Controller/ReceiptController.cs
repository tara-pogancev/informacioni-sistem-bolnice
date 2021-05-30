using SIMS.DTO;
using SIMS.Model;
using SIMS.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Controller
{
    public class ReceiptController
    {
        private ReceiptService receiptService = new ReceiptService();

        public ReceiptController()
        {

        }

        public List<Receipt> GetAllReceipts() => receiptService.GetAllReceipts();

        public void UpdateReceipt(Receipt receipt) => receiptService.UpdateReceipt(receipt);

        public void DeleteReceipt(String key) => receiptService.DeleteReceipt(key);

        public void SaveReceipt(Receipt receipt) => receiptService.SaveReceipt(receipt);

        public Receipt GetReceipt(String key) => receiptService.GetReceipt(key);

        public List<Receipt> ReadByPatient(Patient patient)
        {
            return receiptService.ReadByPatient(patient);
        }

        public ReceiptDTO GetDTO(Receipt receipt)
        {
            return receiptService.GetDTO(receipt);
        }

        public List<ReceiptDTO> GetDTOFromList(List<Receipt> list)
        {
            return receiptService.GetDTOFromList(list);
        }

    }
}
