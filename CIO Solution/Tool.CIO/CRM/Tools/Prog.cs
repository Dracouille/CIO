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

namespace Tools.CIO.Tools
{
    public class Prog
    {
        //Provides a persistent client-to-CRM server communication channel.
        private HttpClient httpClient;
        ApiVersion apiVersion = new ApiVersion();
        ListContact Contacts = new ListContact();
            

        //Test SSIS
        public ListContact Lance()
        {
            ReqAPI ReqContact = new ReqAPI();
            try
            {
                ConnectToCRM();
                Task.WaitAll(Task.Run(async () => await RunAsync(ReqContact)));
            }
            catch (System.Exception ex) { DisplayException(ex); }
            finally
            {
                if (httpClient != null)
                {
                    httpClient.Dispose();
                }
            }
            return(Contacts);
        }

        //Appel les methodes en Asynk
        public async Task RunAsync(ReqAPI Client)
        {
            try
            {
                await apiVersion.ReqApiVersion(httpClient);
                await Client.GetContact(httpClient, apiVersion.getVersionAPI()); // Sending a request
            }
            catch (Exception ex)
            { DisplayException(ex); }
        }

        //Main
        static public void Main(string[] args)
        {
            Prog app = new Prog();
            ReqAPI Client = new ReqAPI();
            
            try
            {
                app.ConnectToCRM();
                Task.WaitAll(Task.Run(async () => await app.RunAsync(Client)));
            }
            catch (System.Exception ex) { DisplayException(ex); }
            finally
            {
                if (app.httpClient != null)
                { app.httpClient.Dispose(); }
                System.Console.WriteLine("Press <Enter> to exit the program.");
                System.Console.ReadLine();
            }
        }

        //Intancie httpClient
        private void ConnectToCRM()
        {
            //Décla de la Config
            Configuration config = null;

            //Varibales de connexion TEST
            string Login = "antonin.damerval@businessdecision.com";
            string Pwd = "Monaco0419*";
            string URL = "https://businessdecision.crm4.dynamics.com/";
            string AzureID = "d099a944-a49b-4fd2-a66c-74c80a3fb5ac";
            string Redirect = "https://businessdecision.crm4.dynamics.com/api/data/v9.0";

            //Appel la config
            config = new FileConfiguration(Login, Pwd, URL, AzureID, Redirect);


            //Décla Auth, use a HttpClient object to connect to specified CRM Web service.
            Authentication auth = new Authentication(config);
            httpClient = new HttpClient(auth.ClientHandler, true);

            //Define the Web API base address, the max period of execute time, the 
            // default OData version, and the default response payload format.
            httpClient.BaseAddress = new Uri(config.ServiceUrl + "api/data/");
            httpClient.Timeout = new TimeSpan(0, 2, 0);
            httpClient.DefaultRequestHeaders.Add("OData-MaxVersion", "4.0");
            httpClient.DefaultRequestHeaders.Add("OData-Version", "4.0");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary> Helper method to display caught exceptions </summary>
        private static void DisplayException(Exception ex)
        {
            System.Console.WriteLine("The application terminated with an error.");
            System.Console.WriteLine(ex.Message);
            while (ex.InnerException != null)
            {
                System.Console.WriteLine("\t* {0}", ex.InnerException.Message);
                ex = ex.InnerException;
            }
        }
    }
}

