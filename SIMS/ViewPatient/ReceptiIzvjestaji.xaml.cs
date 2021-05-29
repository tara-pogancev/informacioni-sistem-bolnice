using SIMS.ViewPatient.ViewModel;
using System;
using System.Collections.Generic;
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

namespace SIMS.ViewPatient
{
    /// <summary>
    /// Interaction logic for ReceptiIzvjestaji.xaml
    /// </summary>
    public partial class ReceptiIzvjestaji : Page
    {
        private RecipeViewModel receiptViewModel;
        public ReceptiIzvjestaji()
        {
            InitializeComponent();
            receiptViewModel = new RecipeViewModel();
            this.DataContext = receiptViewModel;
        }

        private void terminiTabela_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            receiptViewModel.GetReceipt();
            RecipeFrame.Content = new PrikazRecepta(receiptViewModel);
        }
    }
}
