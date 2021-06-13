using SIMS.PacijentGUI.ViewModel;
using Syncfusion.UI.Xaml.Schedule;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
using SIMS.ViewPatient.ViewModel;

namespace SIMS.PacijentGUI
{
    /// <summary>
    /// Interaction logic for IzvjestajPage.xaml
    /// </summary>
    public partial class IzvjestajPage : Page
    {
        
        public IzvjestajPage()
        {
            
            InitializeComponent();
            this.DataContext = new TherapyReportViewModel();



        }

        

    }
}

