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
using SIMS.Model;
using SIMS.ViewDoctor.Dialogues.GeneratePDF.ViewModel;

namespace SIMS.ViewDoctor.Dialogues.GeneratePDF
{
    /// <summary>
    /// Interaction logic for GeneratePDFForm.xaml
    /// </summary>
    public partial class GeneratePDFForm : Window
    {
        public GeneratePDFForm(Patient patient)
        {
            InitializeComponent();
            DataContext = new GeneratePDFViewModel(patient);
        }

        private void WindowKeyListener(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
            //else if (e.Key == Key.Return)
                //Accept();
        }
    }
}
