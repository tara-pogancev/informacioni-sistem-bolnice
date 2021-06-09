using SIMS.ViewSecretary.ViewModel;
using System.Windows.Controls;

namespace SIMS.ViewSecretary.Account
{
    public partial class ViewAccount : Page
    {
        public ViewAccount()
        {
            InitializeComponent();
            this.DataContext = new ViewAccountViewModel();
        }
    }
}
