using SIMS.Repositories.PatientRepo;
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

namespace SIMS.LekarGUI.Pages
{
    /// <summary>
    /// Interaction logic for LDBNemaTermin.xaml
    /// </summary>
    public partial class LDBNemaTermin : Page
    {
        public static LDBNemaTermin instance;

        public static LDBNemaTermin GetInstance()
        {
            if (instance == null)
            {
                instance = new LDBNemaTermin();
            }
            return instance;
        }


        public LDBNemaTermin()
        {
            InitializeComponent();
        }
    }
}
