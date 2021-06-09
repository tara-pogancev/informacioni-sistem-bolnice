using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SIMS.Controller;

namespace SIMS.ViewManager
{
    /// <summary>
    /// Interaction logic for FeedbackPage.xaml
    /// </summary>
    public partial class FeedbackPage : Page
    {
        FeedbackController feedbackController = new FeedbackController();
        public FeedbackPage()
        {
            InitializeComponent();
        }

        private void Posalji_Click(object sender, RoutedEventArgs e)
        {
            feedbackController.Send(InputText.Text);
            
        }
    }
}
