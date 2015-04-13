using Cartographie.DBLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CommercialCompanion
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class ConnexionWindow : Window
    {
        private OVCartographie ovCartographie = new OVCartographie();

        public ConnexionWindow()
        {
            InitializeComponent();

            InitialisationEnvironnement();
        }

        private void InitialisationEnvironnement()
        {
            this.DataContext = ovCartographie;
        }

        private void btnConnexion_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Session> lstSession = ovCartographie.DbSession;

                if(this.ValiderAuthentification())
                {
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.DefinirSessionEnCours(ObtenirSessionEnCours());
                    mainWindow.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur: " + ex.Message);
            }
        }

        private Session ObtenirSessionEnCours()
        {
            return ovCartographie.DbSession.Find(x => x.Login == txtBoxLogin.Text);
        }

        private bool ValiderAuthentification()
        {
            return (ObtenirSessionEnCours() != null && ObtenirSessionEnCours().Password == pwbPassword.Password);
        }

    }
}
