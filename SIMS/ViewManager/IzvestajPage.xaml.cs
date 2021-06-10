using SIMS.ViewManager.ViewModel;
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

namespace SIMS.ViewManager
{
    /// <summary>
    /// Interaction logic for IzvestajPage.xaml
    /// </summary>
    public partial class IzvestajPage : Page
    {
        public IzvestajPage()
        {
            InitializeComponent();
            this.DataContext = new IzvestajPageViewModel();
        }

    }
}
