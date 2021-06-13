using SIMS.Repositories.SecretaryRepo;
using SIMS.Repositories.AllergenRepo;
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
using SIMS.Model;
using SIMS.Controller;
using SIMS.ViewManager.ViewModel;

namespace SIMS.UpravnikGUI
{
    /// <summary>
    /// Interaction logic for AlergeniDetailPage.xaml
    /// </summary>
    public partial class AlergeniDetailPage : Page
    {

        public AlergeniDetailPage(string ID) //izmena postojecg alergena
        {
            InitializeComponent();
            this.DataContext = new AlergeniDetailPageViewModel(ID);
        }

        public AlergeniDetailPage() //nov alergen
        {
            InitializeComponent();
            this.DataContext = new AlergeniDetailPageViewModel();
        }
    }
}
