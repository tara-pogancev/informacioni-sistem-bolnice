using SIMS.Model;
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
using System.Windows.Shapes;
using SIMS.Controller;
using SIMS.ViewDoctor.Dialogues.Izvestaji.ViewModel;

namespace SIMS.LekarGUI.Dialogues.Izvestaji
{
    /// <summary>
    /// Interaction logic for OperacijaIzvestajView.xaml
    /// </summary>
    public partial class SurgeryReportRead : Window
    {
        private PatientController patientController = new PatientController();

        public SurgeryReportRead(SurgeryReport report)
        {
            InitializeComponent();
            DataContext = new ReadSurgeryReportViewModel(report);
        }

        private void WindowKeyListener(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
    }
}
