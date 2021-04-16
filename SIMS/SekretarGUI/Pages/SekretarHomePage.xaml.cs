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

namespace SIMS.SekretarGUI
{
    /// <summary>
    /// Interaction logic for SekretarHomePage.xaml
    /// </summary>
    public partial class SekretarHomePage : Page
    {
        private static SekretarHomePage instance = null;

        public static SekretarHomePage GetInstance()
        {
            if (instance == null)
                instance = new SekretarHomePage();
            return instance;
        }

        private SekretarHomePage()
        {
            InitializeComponent();
        }

        public void RemoveInstance()
        {
            instance = null;
        }
    }
}
