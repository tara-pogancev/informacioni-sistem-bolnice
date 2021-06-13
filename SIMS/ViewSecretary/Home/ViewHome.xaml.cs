using SIMS.Model;
using System.Resources;
using System.Windows.Controls;

namespace SIMS.ViewSecretary.Home
{
    public partial class ViewHome : Page
    {
        private static ViewHome _instance = null;

        public static ViewHome GetInstance()
        {
            return _instance;
        }
        public static ViewHome GetInstance(Secretary secretary)
        {
            if (_instance == null)
                _instance = new ViewHome(secretary);
            return _instance;
        }

        private ViewHome(Secretary secretary)
        {
            InitializeComponent();

            secretaryNameText.Text = secretary.FullName;
        }

        public void RemoveInstance()
        {
            _instance = null;
        }
    }
}
