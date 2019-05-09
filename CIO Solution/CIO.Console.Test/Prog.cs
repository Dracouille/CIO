using System;
using System.Threading.Tasks;
using Tool.CIO.CRM.Connect;
using Tool.CIO.CRM.Tools;
using Tool.CIO.Log;

namespace Tools.CIO.Tools
{
    public class Toto
    {
        //Provides a persistent client-to-CRM server communication channel.
        ListContact Contacts = new ListContact();
        //ConnectionCRM Co = new ConnectionCRM("antonin.damerval@businessdecision.com", "Monaco0419*", "https://businessdecision.crm4.dynamics.com/", "d099a944-a49b-4fd2-a66c-74c80a3fb5ac", "https://businessdecision.crm4.dynamics.com/api/data/v9.0");
        ConnectionCRM Co = new ConnectionCRM("core.service.acc@olimpico.biz", "LS1setup!!", "https://iocdwtest2.crm4.dynamics.com/", "0ce3de8a-9ee5-4dd6-8a3f-2b9a208634cc", "https://localhost", "Z139u@]Ns/?JvYJWm3H2ZiLWJQ+=ee6Y");
        DisplayErr Err = new DisplayErr();
            
        //Prog de test
        public string Lance()
        {
            //Déclare une nouvelle requete
            ReqAPI ReqContact = new ReqAPI();
            try
            {
                //Ouvre la connexion
                Task.WaitAll(Task.Run(async () => await RunAsync(ReqContact)));
            }
            catch (System.Exception ex) { Err.DisplayException(ex); }
            finally
            {
                if (Co.GetHTTPClient() != null)
                {
                    Co.Déconnecte();
                }
            }

            Console.WriteLine(Contacts.GetPerson[0].m_firstname);
            Console.WriteLine(Contacts.GetPerson[1].m_firstname);
            Console.WriteLine(Contacts.GetPerson[2].m_firstname);
            Console.ReadKey();

            return (Contacts.GetPerson[0].m_firstname);
        }

        //Appel les requetes en Asynk
        public async Task RunAsync(ReqAPI Client)
        {
            try
            {
                Contacts = await Client.GetContact(Co); // Sending a request
            }
            catch (Exception ex)
            { Err.DisplayException(ex); }
        }

        //Main
        static public void Main(string[] args)
        {
            Toto app = new Toto();

            app.RecupConfig();
            app.Lance();
        }

        //Lance la récupération pour la connexion au serveur
        public void RecupConfig()
        {
            Co.ConnectToCRM(Err);
        }
    }
}

