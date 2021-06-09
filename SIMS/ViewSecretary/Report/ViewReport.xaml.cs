using SIMS.ViewSecretary.ViewModel;
using System.Windows.Controls;

namespace SIMS.ViewSecretary.Report
{
    public partial class ViewReport : Page
    {
        public ViewReport()
        {
            InitializeComponent();
            this.DataContext = new ViewReportViewModel();
        }
    }
}
