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
using SIMS.LekarGUI.Dialogues.Izvestaji;

namespace SIMS.LekarGUI.Pages
{
    /// <summary>
    /// Interaction logic for LDBAktivanTermin.xaml
    /// </summary>
    public partial class LDBAktivanTermin : Page
    {
        public static LDBAktivanTermin instance;
        private static Termin aktivanTermin;

        public static LDBAktivanTermin GetInstance(Termin t)
        {
            if (instance == null)
            {
                instance = new LDBAktivanTermin();
                aktivanTermin = t;
            }
            return instance;
        }

        public LDBAktivanTermin()
        {
            InitializeComponent();
        }

        private void Evidentiraj_Button(object sender, RoutedEventArgs e)
        {
            LekarUI.GetInstance().ChangeTab(3);

            if (aktivanTermin.VrstaTermina == TipTermina.pregled)
            {
                AnamnezaCreate a = new AnamnezaCreate(aktivanTermin);
                a.ShowDialog();
            }
            else
            {
                OperacijaIzvestajCreate o = new OperacijaIzvestajCreate(aktivanTermin);
                o.ShowDialog();
            }
        }
    }
}
