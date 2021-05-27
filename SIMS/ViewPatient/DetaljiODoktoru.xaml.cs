using SIMS.ViewPatient.ViewModel;
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

namespace SIMS.ViewPatient
{
    /// <summary>
    /// Interaction logic for DetaljiODoktoru.xaml
    /// </summary>
    public partial class DetaljiODoktoru : Page
    {
        public DetaljiODoktoru(DoctorDetailsViewModel detailsViewModel)
        {
            InitializeComponent();
            this.DataContext =detailsViewModel;
        }
    }
}
