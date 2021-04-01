using Model;
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
    /// Interaction logic for LekarEvidencijaPage.xaml
    /// </summary>
    public partial class LekarEvidencijaPage : Page
    {
        public static LekarEvidencijaPage instance;

        private static Lekar lekarUser;

        public static LekarEvidencijaPage GetInstance(Lekar l)
        {
            if (instance == null)
            {
                lekarUser = l;
                instance = new LekarEvidencijaPage();
            }
            return instance;
        }

        public static LekarEvidencijaPage GetInstance()
        {
            return instance;
        }


        public LekarEvidencijaPage()
        {
            InitializeComponent();
        }
    }
}
