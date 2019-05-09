using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Text;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Linq;
using System.Net;
using Tool.CIO.CRM.Connect;
using Tool.CIO.CRM.Tools;
using Tool.CIO.Log;

namespace Tools.CIO.Tools
{
    public class Prog
    {
        //Provides a persistent client-to-CRM server communication channel.
        private HttpClient httpClient;
        ListContact Contacts = new ListContact();
        ConnectionCRM Co = new ConnectionCRM("antonin.damerval@businessdecision.com", "Monaco0419*", "https://businessdecision.crm4.dynamics.com/", "d099a944-a49b-4fd2-a66c-74c80a3fb5ac", "https://businessdecision.crm4.dynamics.com/api/data/v9.0", "Z139u@]Ns/?JvYJWm3H2ZiLWJQ+=ee6Y");
        DisplayErr Err = new DisplayErr();

        //Test SSIS
        public string Lance()
        {
            //Déclare une nouvelle requete
            ReqAPI ReqContact = new ReqAPI();

            try
            {
                //Ouvre la connexion
                httpClient = Co.GetHTTPClient();
                Task.WaitAll(Task.Run(async () => await RunAsync(ReqContact)));
            }
            catch (System.Exception ex) { Err.DisplayException(ex); }
            finally
            {
                if (httpClient != null)
                {
                    httpClient.Dispose();
                }
            }
            return(Contacts.GetPerson[0].m_firstname);
        }

        //Lance la récupération pour la connexion au serveur
        public void RecupConfig()
        {
            Co.ConnectToCRM(Err);
            Task.WaitAll(Task.Run(async () => await Co.RunAsyncApiVersion(Err)));
        }

        //Appel les methodes en Asynk
        public async Task RunAsync(ReqAPI Client)
        {
            try
            {
                Contacts = await Client.GetContact(Co); // Sending a request
            }
            catch (Exception ex)
            { Err.DisplayException(ex); }
        }

    }
}

