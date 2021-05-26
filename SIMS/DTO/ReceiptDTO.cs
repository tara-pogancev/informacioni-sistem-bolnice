using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Controller;
using SIMS.Model;

namespace SIMS.DTO
{
    public class ReceiptDTO
    {
        private ReceiptController receiptController = new ReceiptController();

        public String DoctorName { get; }
        public String DateString { get;  }
        public String MedicationNameAmount { get; }
        public Receipt Receipt { get; set; } 

        public ReceiptDTO(Receipt receipt)
        {
            Receipt = receiptController.GetReceipt(receipt.RecieptID);
            Receipt.InitData();
            DoctorName = Receipt.Doctor.FullName;
            MedicationNameAmount = Receipt.MedicineName + ", " + Receipt.Amount;
        }

    }
}
