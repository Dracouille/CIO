using System;
using System.Threading.Tasks;
using Tool.CIO.CRM.Connect;
using Tool.CIO.CRM.Tools;
using Tool.CIO.Log;

namespace Tools.CIO.Tools
{
    public class Toto
    {
        //ConnectionCRM Co = new ConnectionCRM("antonin.damerval@businessdecision.com", "Monaco0419*", "https://businessdecision.crm4.dynamics.com/", "d099a944-a49b-4fd2-a66c-74c80a3fb5ac", "https://businessdecision.crm4.dynamics.com/api/data/v9.0");
        DisplayErr Err = new DisplayErr();

        //Main
        static public void Main(string[] args)
        {
            Toto app = new Toto();
            ConnectionCRM Co = new ConnectionCRM("core.service.acc@olimpico.biz", "LS1setup!!", "https://iocdwtest2.crm4.dynamics.com/", "0ce3de8a-9ee5-4dd6-8a3f-2b9a208634cc", "https://localhost", "Z139u@]Ns/?JvYJWm3H2ZiLWJQ+=ee6Y");

            Task.WaitAll(Task.Run(async () => await app.Noc(Co)));
            Task.WaitAll(Task.Run(async () => await app.Membre(Co)));
            Task.WaitAll(Task.Run(async () => await app.Federation(Co)));
            Task.WaitAll(Task.Run(async () => await app.Commission(Co)));

            Console.ReadKey();
        }


        //Lance la récupération pour la connexion au serveur
        public void RecupConfig(ConnectionCRM Co)
        {
            Co.ConnectToCRM(Err);
        }

        //Test NOC
        public async Task Noc(ConnectionCRM Co)
        {
            LanceRequete requete = new LanceRequete();
            RecupConfig(Co);
            ListNOC ListNoc = new ListNOC();
            ListNoc = await requete.LanceNOC(Err, Co);

            if (ListNoc != null)
            {
                Console.WriteLine("NOC OK");
            }
            else
            {
                Console.WriteLine("Nul");
            }
        }

        //Test Membre
        public async Task Membre(ConnectionCRM Co)
        {
            LanceRequete requete = new LanceRequete();
            ListMember ListMember = new ListMember();
            RecupConfig(Co);
            ListMember = await requete.LanceMember(Err, Co);

            if (ListMember != null)
            {
                Console.WriteLine("Membre OK");
            }
            else
            {
                Console.WriteLine("Nul");
            }
        }

        //Test Fédé
        public async Task Federation(ConnectionCRM Co)
        {
            LanceRequete requete = new LanceRequete();
            ListFederation ListFede = new ListFederation();
            RecupConfig(Co);
            ListFede = await requete.LanceFederation(Err, Co);

            if (ListFede != null)
            {
                Console.WriteLine("Fede OK");
            }
            else
            {
                Console.WriteLine("Nul");
            }
        }

        //test Commi
        public async Task Commission(ConnectionCRM Co)
        {
            LanceRequete requete = new LanceRequete();
            ListCommission ListCom = new ListCommission();
            RecupConfig(Co);
            ListCom = await requete.LanceCommission(Err, Co);

            if (ListCom != null)
            {
                Console.WriteLine("Commission OK");
            }
            else
            {
                Console.WriteLine("Nul");
            }
        }
    }
}

