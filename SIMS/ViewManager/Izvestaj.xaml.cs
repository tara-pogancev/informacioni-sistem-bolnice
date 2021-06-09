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
using SIMS.ViewManager.ViewModel;

namespace SIMS.ViewManager
{
    /// <summary>
    /// Interaction logic for Izvestaj.xaml
    /// </summary>
    public partial class Izvestaj : Page
    {
        
        public Izvestaj(DateTime intervalStart, DateTime intervalEnd)
        {
            InitializeComponent();
            DataContext = new IzvestajViewModel(intervalStart, intervalEnd);
        }
    }
}
