using Cartographie.DBLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CommercialCompanion
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private OVCartographie ovCartographie = new OVCartographie();

        public MainWindow()
        {
            InitializeComponent();

            InitialisationVariables();
            InitialisationEnvironnement();
        }

        public void DefinirSessionEnCours(Session sessionEnCours)
        {
            ovCartographie.SessionEnCours = sessionEnCours;
        }

        private void InitialisationEnvironnement()
        {
            this.DataContext = ovCartographie;
        }

        private void InitialisationVariables()
        {
            
        }

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                /// On génère une nouvelle liste pour lever l'évènement "Set" lors de l'insertion de la nouvelle ligne
                /// L'add direct dans la liste dbClient ne lève pas l'évènement Set, et donc ne lève pas non plus la sauvegarde
                /// 
                List<Client> lstClient = ovCartographie.DbClient;
                lstClient.Add(new Client { Adresse = "", Code_Postal = "", Complement_Adresse = "", Pays = "", Ville = "", Nom = "", Identifiant_Categorie = 1 });
                ovCartographie.DbClient = lstClient;

                RechargerListeClientsEtSelectionner(ovCartographie.DbClient.Last());

                dgClient.IsEnabled = false;
                dgFormulaire.IsEnabled = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur: " + ex.Message);
            }
        }

        private void RechargerListeClientsEtSelectionner(Client ClientASelectionner)
        {
            dgClient.ItemsSource = null;
            dgClient.ItemsSource = ovCartographie.DbClient;

            dgClient.SelectedItem = ClientASelectionner;
        }

        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {
            List<Client> lstClient = ovCartographie.DbClient;
            lstClient.Add((Client)dgClient.SelectedItem);
            ovCartographie.DbClient = lstClient;

            RechargerListeClientsEtSelectionner(ovCartographie.DbClient.Last());

            dgClient.IsEnabled = false;
            dgFormulaire.IsEnabled = true;
        }

        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            List<Client> lstClient = ovCartographie.DbClient;
            lstClient.Remove((Client)dgClient.SelectedItem);
            ovCartographie.DbClient = lstClient;

            RechargerListeClientsEtSelectionner(ovCartographie.DbClient.Last());
        }

        private void dpHeader_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnValider_Click(object sender, RoutedEventArgs e)
        {
            List<Client> lstClient = (List<Client>)dgClient.ItemsSource;

            ovCartographie.DbClient = lstClient;

            dgClient.ItemsSource = null;
            dgClient.ItemsSource = ovCartographie.DbClient;

            dgFormulaire.IsEnabled = false;
            dgClient.IsEnabled = true;
        }

        private void btnReduce_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnDeconnexion_Click(object sender, RoutedEventArgs e)
        {
            ConnexionWindow connexionWindow = new ConnexionWindow();
            connexionWindow.Show();
            this.Close();
        }
    }
}
