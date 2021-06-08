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
    /// Interaction logic for AccountPreferences.xaml
    /// </summary>
    public partial class AccountPreferences : Window
    {
        private Doctor doctorUser = DoctorUI.GetInstance().GetUser();

        public AccountPreferences()
        {
            InitializeComponent();
            NameBox.Text = doctorUser.Name;
            LastNameBox.Text = doctorUser.LastName;
            UsernameBox.Text = doctorUser.Username;
            EmailBox.Text = doctorUser.Email;
            PhoneBox.Text = doctorUser.Phone;
            AddressBox.Text = doctorUser.AddressString;
        }

        private void ButtonDecline(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonAccept(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonPasswordChange(object sender, RoutedEventArgs e)
        {
            new ChangePassword().ShowDialog();
        }

        private void WindowKeyListener(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
            else if (e.Key == Key.Return)
                Close();
        }
    }
}
