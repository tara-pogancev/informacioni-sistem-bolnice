using SIMS.Repositories.SecretaryRepo;
using SIMS.LekarGUI.Dialogues.Materijali_i_lekovi;
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
using SIMS.Model;
using System.Collections.ObjectModel;

namespace SIMS.LekarGUI
{
    /// <summary>
    /// Interaction logic for LekarEvidencijaPage.xaml
    /// </summary>
    /// 
    public class MaterialsPlaceholderClass
    {
        public String Material { get; set; }
        public String Amount { get; set; }

        public MaterialsPlaceholderClass(String material, String amount)
        {
            Material = material;
            Amount = amount;
        }
    }

    public partial class DoctorInverntoyPage : Page
    {
        public ObservableCollection<MaterialsPlaceholderClass> MaterialsView { get; set; }
        private List<String> roomNames = new List<string>();

        public DoctorInverntoyPage()
        {
            InitializeComponent();
            DataContext = this;

            MaterialsView = new ObservableCollection<MaterialsPlaceholderClass>();
            MaterialsView.Add(new MaterialsPlaceholderClass("Gaza S", "36"));
            MaterialsView.Add(new MaterialsPlaceholderClass("Kompresivni zavoj L", "14"));
            MaterialsView.Add(new MaterialsPlaceholderClass("Trougao marama", "16"));
            MaterialsView.Add(new MaterialsPlaceholderClass("Makaze", "2"));
            MaterialsView.Add(new MaterialsPlaceholderClass("Brufen 200 pakovanje", "3"));

            roomNames.Add("Operaciona sala 48A");
            roomNames.Add("Bolesnička soba 36");
            roomNames.Add("Bolesnička soba 18");

            RoomCombo.ItemsSource = roomNames;
        }

        private void Button_Home(object sender, MouseButtonEventArgs e)
        {
            DoctorUI.GetInstance().ChangeTab(0);
        }

        private void ButtonMaterialConsumption(object sender, RoutedEventArgs e)
        {
            new PotrosnjaMaterijala().ShowDialog();
        }
    }

}
