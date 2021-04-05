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
using Model;

namespace SIMS.LekarGUI
{
    /// <summary>
    /// Interaction logic for PacijentKartonView.xaml
    /// </summary>
    public partial class PacijentKartonView : Page
    {
        private Pacijent pacijentProfile;

        public static PacijentKartonView instance;

        public static PacijentKartonView GetInstance(Pacijent p)
        {
            instance = new PacijentKartonView(p);
            return instance;
        }

        public static PacijentKartonView GetInstance()
        {
            return instance;
        }

        public PacijentKartonView(Pacijent p)
        {
            InitializeComponent();

            pacijentProfile = p;

            


        }
    }
}
