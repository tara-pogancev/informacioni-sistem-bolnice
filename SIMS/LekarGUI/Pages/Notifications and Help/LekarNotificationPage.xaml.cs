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
    /// Stranica u glavnom prozoru koja se odnosi na nalog lekara, uz mogucnosti podesavanja profila.
    /// </summary>
    /// 

    public partial class LekarNotificationPage : Page
    {
        public static LekarNotificationPage instance;

        private static Lekar lekarUser;

        public static LekarNotificationPage GetInstance(Lekar l)
        {
            if (instance == null)
            {
                lekarUser = l;
                instance = new LekarNotificationPage();
            }
            return instance;
        }

        public static LekarNotificationPage GetInstance()
        {
            return instance;
        }

        public LekarNotificationPage()
        {
            InitializeComponent();
        }

        public void RemoveInstance()
        {
            instance = null;
        }
    }
}
