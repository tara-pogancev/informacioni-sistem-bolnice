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
using SIMS.Repositories.SecretaryRepo;
using SIMS.Daemon.PremestajOpreme;
using SIMS.Model;
using SIMS.Controller;
using SIMS.DTO;
using SIMS.ViewManager.ViewModel;

namespace SIMS.UpravnikGUI
{
    /// <summary>
    /// Interaction logic for UpravnikPremestiOpremu.xaml
    /// </summary>
    public partial class UpravnikPremestiOpremu : Page
    {
        public UpravnikPremestiOpremu(UpravnikInventarProstorijePage ParentPage, string BrojProstorije, Inventory Oprema)
        {
            this.DataContext = new PremestiOpremuPageViewModel(ParentPage, BrojProstorije, Oprema);
            InitializeComponent();
        }
    }
}
