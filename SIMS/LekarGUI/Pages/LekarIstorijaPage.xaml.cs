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
    /// Interaction logic for LekarIstorijaPage.xaml
    /// </summary>
    public partial class LekarIstorijaPage : Page
    {
        public static LekarIstorijaPage instance;

        private static Lekar lekarUser;

        public static LekarIstorijaPage GetInstance(Lekar l)
        {
            if (instance == null)
            {
                lekarUser = l;
                instance = new LekarIstorijaPage();
            }
            return instance;
        }

        public static LekarIstorijaPage GetInstance()
        {
            return instance;
        }


        public LekarIstorijaPage()
        {
            InitializeComponent();
        }
    }
}
