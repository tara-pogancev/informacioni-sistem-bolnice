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
            return instance;
        }
        public static SekretarHomePage GetInstance(Sekretar sekretar)
        {
            if (instance == null)
                instance = new SekretarHomePage(sekretar);
            return instance;
        }

        private SekretarHomePage(Sekretar sekretar)
        {
            InitializeComponent();
            welcomeText.Text = "Dobrodosli,\n" + sekretar.ImePrezime;
        }

        public void RemoveInstance()
        {
            instance = null;
        }
    }
}
