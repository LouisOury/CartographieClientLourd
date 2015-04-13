using Cartographie.DBLib;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialCompanion
{
    public class OVCartographie
    {
        CartographieBDDEntities db = new CartographieBDDEntities();

        public List<Categorie> DbCategorie
        {
            get { return db.Categories.ToList<Categorie>(); }
            set
            {
                foreach (Categorie item in value)
                {
                    db.Categories.Add(item);
                }

                db.SaveChanges();
            }
        }
        public List<Client> DbClient
        {
            get { return db.Clients.ToList<Client>(); }
            set
            {
                List<Client> lstClient;

                /// Création des clients qui n'éxistent pas en base
                lstClient = value.Except(db.Clients.ToList<Client>(), new ClientComparer()).ToList<Client>();
                db.Clients.AddRange(lstClient);

                /// Suppression des clients qui n'existent plus
                lstClient = db.Clients.ToList<Client>().Except(value, new ClientComparer()).ToList<Client>();
                db.Clients.RemoveRange(lstClient);

                //Sauvegarde la liste
                db.SaveChanges();
            }
        }

        public List<Session> DbSession
        {
            get { return db.Sessions.ToList<Session>(); }
        }

        public Session SessionEnCours { get; set; }
    }

    //Copmparer les deux listes
    public class ClientComparer : IEqualityComparer<Client>
    {
        bool IEqualityComparer<Client>.Equals(Client x, Client y)
        {
            return (x.Identifiant.Equals(y.Identifiant));
        }

        int IEqualityComparer<Client>.GetHashCode(Client obj)
        {
            if (Object.ReferenceEquals(obj, null))
                return 0;

            return obj.Identifiant.GetHashCode();
        }
    }

}
