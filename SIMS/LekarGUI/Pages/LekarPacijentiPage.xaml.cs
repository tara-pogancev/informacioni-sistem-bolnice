using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for LekarPacijentiPage.xaml
    /// </summary>
    public partial class LekarPacijentiPage : Page
    {
        public static LekarPacijentiPage instance;

        private static Lekar lekarUser;

        public static LekarPacijentiPage GetInstance(Lekar l)
        {
            if (instance == null)
            {
                lekarUser = l;
                instance = new LekarPacijentiPage();
            }
            return instance;
        }

        public static LekarPacijentiPage GetInstance()
        {
            return instance;
        }

        public LekarPacijentiPage()
        {
            InitializeComponent();
        }
    }
}
