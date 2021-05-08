using Model;
using System.Windows.Controls;

namespace SIMS.SekretarGUI
{
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
