using SIMS.PacijentGUI.ViewModel;
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

namespace SIMS.PacijentGUI
{
    /// <summary>
    /// Interaction logic for ListaIzborLjekaraPage.xaml
    /// </summary>
    public partial class ListaIzborLjekaraPage : Page
    {
        public ListaIzborLjekaraPage()
        {
            InitializeComponent();
            this.DataContext = new ListaLjekaraPageViewModel();
        }
    }
}
