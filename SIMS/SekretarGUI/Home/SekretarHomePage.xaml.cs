using SIMS.Repositories.SecretaryRepo;
using System.Windows.Controls;

namespace SIMS.SekretarGUI
{
    public partial class SekretarHomePage : Page
    {
        private static SekretarHomePage _instance = null;

        public static SekretarHomePage GetInstance()
        {
            return _instance;
        }
        public static SekretarHomePage GetInstance(Secretary secretary)
        {
            if (_instance == null)
                _instance = new SekretarHomePage(secretary);
            return _instance;
        }

        private SekretarHomePage(Secretary secretary)
        {
            InitializeComponent();
            welcomeText.Text = "Dobrodosli,\n" + secretary.FullName;
        }

        public void RemoveInstance()
        {
            _instance = null;
        }
    }
}
