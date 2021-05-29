using SIMS.Repositories.SecretaryRepo;
using SIMS.Repositories.AllergenRepo;
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
using SIMS.Controller;

namespace SIMS.UpravnikGUI
{
    /// <summary>
    /// Interaction logic for AlergeniDetailPage.xaml
    /// </summary>
    public partial class AlergeniDetailPage : Page
    {
        Allergen allergen;
        AllergenController allergenController = new AllergenController();

        public AlergeniDetailPage(string ID) //izmena postojecg alergena
        {
            allergen = allergenController.GetAllergen(ID);
            InitializeComponent();

            NazivText.Text = allergen.Name;
            IDText.Text = allergen.ID;
            IDText.IsEnabled = false;
        }

        public AlergeniDetailPage() //nov alergen
        {
            allergen = new Allergen();
            InitializeComponent();
        }


        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            UpravnikWindow.Instance.SetContent(new AlergeniPage());
            UpravnikWindow.Instance.SetLabel("Alergeni");
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            allergen.Name = NazivText.Text;
            allergen.ID = IDText.Text;

            allergenController.CreateOrUpdate(allergen);

            UpravnikWindow.Instance.SetContent(new AlergeniPage());
            UpravnikWindow.Instance.SetLabel("Alergeni");
        }
    }
}
