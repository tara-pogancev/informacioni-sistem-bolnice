using SIMS.Commands;
using SIMS.ViewSecretary.Home;
using SIMS.ViewSecretary.Report;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace SIMS.ViewSecretary.ViewModel
{
    public class ViewReportViewModel : ViewModelSecretary
    {
        public DateTime SelectedDateStart { get; set; }
        public string SelectedDateTextStart { get; set; }
        public DateTime SelectedDateEnd { get; set; }
        public string SelectedDateTextEnd { get; set; }
        public RelayCommand GenerateReportCommand { get; set; }
        public RelayCommand QuitCommand { get; set; }
        public ViewReportViewModel()
        {
            GenerateReportCommand = new RelayCommand(Execute_GenerateReportCommand, CanExecute_GenerateReportCommand);
            QuitCommand = new RelayCommand(Execute_QuitCommand, CanExecute_QuitCommand);
            SelectedDateStart = DateTime.Now;
            SelectedDateTextStart = SelectedDateStart.ToString();
            SelectedDateEnd = DateTime.Now;
            SelectedDateTextEnd = SelectedDateEnd.ToString();
        }

        private void Execute_GenerateReportCommand(object obj)
        {
            PrintDialog printDialog = new PrintDialog();
            ReportToGenerate rtg = new ReportToGenerate(SelectedDateStart, SelectedDateEnd);
            /*rtg.ReportScroll.ScrollToTop();
            printDialog.PrintVisual(rtg.ReportScroll.Content as Visual, "IzvestajOZauzetostiProstorija");*/
            FlowDocument fd = rtg.Document;
            DocumentPaginator documentPaginator = (fd as IDocumentPaginatorSource).DocumentPaginator;
            printDialog.PrintDocument(documentPaginator, "Izvestaj");

            SecretaryUI.GetInstance().Caption.Content = TranslationSource.Instance["HomePageListItem"];
            SecretaryUI.GetInstance().MainFrame.NavigationService.Navigate(ViewHome.GetInstance());
        }

        private bool CanExecute_GenerateReportCommand(object obj)
        {
            return true;
        }

        private void Execute_QuitCommand(object obj)
        {
            SecretaryUI.GetInstance().Caption.Content = TranslationSource.Instance["HomePageListItem"];
            SecretaryUI.GetInstance().MainFrame.NavigationService.Navigate(ViewHome.GetInstance());
        }

        private bool CanExecute_QuitCommand(object obj)
        {
            return true;
        }
    }
}
