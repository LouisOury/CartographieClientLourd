using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using Cartographie.DBLib;

namespace CartographieClientLourd
{
    public partial class GestionClient : Form
    {
        private List<Client> listClients = null;
        private ObservableCollection<Categorie> listCat = null;

        CartographieBDDEntities db = new CartographieBDDEntities();

        public GestionClient()
        {
            InitializeComponent();

            listClients = new List<Client>();
            clientDataGridView.DataSource = db.Clients.ToList();

            listCat = new ObservableCollection<Categorie>();
            categorieComboBox.DataSource = db.Categories.ToList();
            categorieComboBox.DisplayMember = "Identifiant";
            categorieComboBox.ValueMember = "Identifiant";

            foreach (DataGridViewRow row in clientDataGridView.SelectedRows)
            {
                nomTextBox.Text = row.Cells["Nom"].Value.ToString();
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                listClients = new List<Client>();

                //Ajout valeur a variable
                listClients.Add(new Client()
                {
                    Nom = nomTextBox.Text,
                    Adresse = adresseTextBox.Text,
                    Complement_Adresse = compAdresseTextBox.Text,
                    Code_Postal = cpTextBox.Text,
                    Ville = villeTextBox.Text,
                    Pays = paysTextBox.Text,
                    Dimension_Entreprise = dimEntTextBox.Text,
                    Url_Site = urlTextBox.Text,
                    Identifiant_Categorie = (int)categorieComboBox.SelectedValue
                });

                //Ajout client
                foreach (Client item in listClients)
                {
                    db.Clients.Add(item);
                }
                db.SaveChanges(); //Sauvegarde client

                //Refresh DataGridView
                clientDataGridView.DataSource = db.Clients.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur: " + ex.Message);
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            listClients = new List<Client>();
            if(clientDataGridView.SelectedRows.Count > 0 && clientDataGridView.SelectedRows.Count <= 1)
            {
                Client clientSelect = (Client)clientDataGridView.SelectedRows[0].DataBoundItem;
                db.Clients.Remove(clientSelect);
                db.SaveChanges();
            }

            //Refresh DataGridView
            clientDataGridView.DataSource = db.Clients.ToList();
        }

        private void GestionClient_Load(object sender, EventArgs e)
        {
            // TODO: cette ligne de code charge les données dans la table 'cartographieBDDDataSet.Client'. Vous pouvez la déplacer ou la supprimer selon vos besoins.
            this.clientTableAdapter.Fill(this.cartographieBDDDataSet.Client);
        }
    }
}
