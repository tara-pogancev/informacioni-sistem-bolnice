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

namespace SIMS.LekarGUI.Dialogues.Izmena_naloga
{
    /// <summary>
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Window
    {
        private Doctor doctorUser = DoctorUI.GetInstance().GetUser();

        public ChangePassword()
        {
            InitializeComponent();
            passBox.Password = doctorUser.Password;
        }

        private void ButtonAccept(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonDecline(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
