using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SIMS.Repositories.SecretaryRepo;
using SIMS.LekarGUI.Dialogues.Izvestaji;
using SIMS.Model;
using SIMS.Repositories.AnamnesisRepository;
using SIMS.DTO;
using SIMS.Controller;
using SIMS.ViewDoctor.Dialogues.GeneratePDF;

namespace SIMS.LekarGUI
{
    /// <summary>
    /// Interaction logic for PacijentKartonView.xaml
    /// </summary>
    public partial class PatientDocumentationView : Page
    {
        private Patient patient;

        public ObservableCollection<AnamnesisDTO> AnamnesisViewModel { get; set; }
        public ObservableCollection<SurgeryReportDTO> SurgeryReportViewModel { get; set; }
        public ObservableCollection<ReceiptDTO> ReceiptViewModel { get; set; }

        private AnamnesisController anamnesisController = new AnamnesisController();
        private SurgeryReportController surgeryReportController = new SurgeryReportController();
        private ReceiptController receiptController = new ReceiptController();

        public PatientDocumentationView(Patient patient)
        {
            InitializeComponent();

            this.patient = patient;
            this.DataContext = this;
            InitializeData();

            LabelNameTop.Content = this.patient.FullName;
            LabelName.Content = this.patient.FullName;

        }

        private void InitializeData()
        {
            AnamnesisViewModel = new ObservableCollection<AnamnesisDTO>(anamnesisController.GetDTOFromList(anamnesisController.GetAnamnesisByPatient(patient)));
            ReceiptViewModel = new ObservableCollection<ReceiptDTO>(receiptController.GetDTOFromList(receiptController.ReadByPatient(patient)));
            SurgeryReportViewModel = new ObservableCollection<SurgeryReportDTO>(surgeryReportController.GetDTOFromList(surgeryReportController.ReadByPatient(patient)));
        }

        private void ButtonPatientsView(object sender, MouseButtonEventArgs e)
        {
            DoctorUI.GetInstance().ChangeTab(2);
        }

        private void ButtonHome(object sender, MouseButtonEventArgs e)
        {
            DoctorUI.GetInstance().ChangeTab(0);
        }

        private void ButtonPatientHealthRecord(object sender, MouseButtonEventArgs e)
        {
            DoctorUI.GetInstance().SellectedTab.Content = new PatientRecordCheck(patient);
        }

        private void ButtonRead(object sender, RoutedEventArgs e)
        {
            if (TabbedPanel.SelectedIndex == 0)
            {
                ReadAnamnesis();
            }
            else if (TabbedPanel.SelectedIndex == 1)
            {
                ReadSurgeryReport();
            }
            else if (TabbedPanel.SelectedIndex == 2)
            {
                ReadReceipt();
            }
        }

        private void ButtonGenerateReportFile(object sender, RoutedEventArgs e)
        {
            new GeneratePDFForm(patient).ShowDialog();
        }

        private void ButtonViewReceipt(object sender, MouseButtonEventArgs e)
        {
            ReadReceipt();
        }

        private void Button_ViewPregled(object sender, MouseButtonEventArgs e)
        {
            ReadAnamnesis();
        }

        private void ButtonReadSurgeryReport(object sender, MouseButtonEventArgs e)
        {
            ReadSurgeryReport();
        }

        private void ReadReceipt()
        {
            if (dataGridReceipts.SelectedItem != null)
            {
                Receipt selectedReceipt = GetSelectedReceipt();
                new PrikazRecepta(selectedReceipt).Show();
            }
        }

        private void ReadAnamnesis()
        {
            if (dataGridAnamnesis.SelectedItem != null)
            {
                Anamnesis selectedAnamnesis = GetSelectedAnamnesis();
                new AnamnesisRead(selectedAnamnesis).Show();
            }
        }

        private void ReadSurgeryReport()
        {
            if (dataGridSurgery.SelectedItem != null)
            {
                SurgeryReport selectedReport = GetSelectedReport();
                new SurgeryReportRead(selectedReport).Show();
            }
        }

        private Anamnesis GetSelectedAnamnesis()
        {
            AnamnesisDTO dto = (AnamnesisDTO)dataGridAnamnesis.SelectedItem;
            return anamnesisController.GetAnamnesis(dto.AnamnesisID);
        }

        private Receipt GetSelectedReceipt()
        {
            ReceiptDTO dto = (ReceiptDTO)dataGridReceipts.SelectedItem;
            return receiptController.GetReceipt(dto.Receipt.RecieptID);
        }

        private SurgeryReport GetSelectedReport()
        {
            SurgeryReportDTO dto = (SurgeryReportDTO)dataGridSurgery.SelectedItem;
            return surgeryReportController.GetReport(dto.ReportID);
        }
    }
}
