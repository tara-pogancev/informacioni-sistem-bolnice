using SIMS.LekarGUI.Dialogues.CoffeeBreak;
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

namespace SIMS.LekarGUI
{
    /// <summary>
    /// Interaction logic for LekarHelpPage.xaml
    /// </summary>
    public partial class LekarHelpPage : Page
    {
        public LekarHelpPage()
        {
            InitializeComponent();
        }

        private void Button_Home(object sender, MouseButtonEventArgs e)
        {
            DoctorUI.GetInstance().ChangeTab(0);
        }

        private void CoffeeBreak(object sender, MouseButtonEventArgs e)
        {
            new CoffeeBreakWindow().Show();
        }
    }
}
