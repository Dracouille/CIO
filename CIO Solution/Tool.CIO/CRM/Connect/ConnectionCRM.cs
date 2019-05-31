using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Tool.CIO.Log;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System.ServiceModel.Description;
using System.ServiceModel;


//Varibales de connexion TEST
//string Login = "antonin.damerval@businessdecision.com";
//string Pwd = "Monaco0419*";
//string URL = "https://businessdecision.crm4.dynamics.com/";
//string AzureID = "d099a944-a49b-4fd2-a66c-74c80a3fb5ac";
//string Redirect = "https://businessdecision.crm4.dynamics.com/api/data/v9.0";


namespace Tool.CIO.CRM.Connect
{
    public class ConnectionCRM
    {
        #region Members
        private HttpClient httpClient;
        private Version webAPIVersion = new Version(8, 0); //Start with base version and update with actual version later.
        private string Login, Pwd, URL, AzureID, Redirect, SecretKey;
        #endregion

        #region Properties
        //Retourne la connexion
        public HttpClient GetHTTPClient()
        {
            return(httpClient);
        }

        //Retourne la version de l'api
        public string getVersionAPI()
        {
            return string.Format("v{0}/", webAPIVersion.ToString(2));
        }
        
        //récupere la version de l'api
        public async Task ReqApiVersion(HttpClient httpClient)
        {
            HttpRequestMessage RetrieveVersionRequest = new HttpRequestMessage(HttpMethod.Get, getVersionAPI() + "RetrieveVersion()");

            HttpResponseMessage RetrieveVersionResponse = await httpClient.SendAsync(RetrieveVersionRequest);
            if (RetrieveVersionResponse.StatusCode == HttpStatusCode.OK)  //200
            {
                JObject RetrievedVersion = JsonConvert.DeserializeObject<JObject>(await RetrieveVersionResponse.Content.ReadAsStringAsync());
                //Capture the actual version available in this organization
                webAPIVersion = Version.Parse((string)RetrievedVersion.GetValue("Version"));
            }
            else
            {
                System.Console.WriteLine("Failed to retrieve the version for reason: {0}",
                    RetrieveVersionResponse.ReasonPhrase);
                throw new CrmHttpResponseException(RetrieveVersionResponse.Content);
            }
        }
        #endregion

        #region Constructeur
        public ConnectionCRM(string p_Login, string p_Pwd, string p_URL, string p_AzureID, string p_Redirect, string p_SecretKey)
        {
            #region CheckParams
            if (string.IsNullOrWhiteSpace(p_URL) || !Uri.IsWellFormedUriString(p_URL, UriKind.Absolute))
                throw new ArgumentException("L'url du CRM n'est pas valide");
            if (string.IsNullOrWhiteSpace(p_Login))
                throw new ArgumentException("Le login ne doit pas être null ou vide");
            if (string.IsNullOrWhiteSpace(p_Pwd))
                throw new ArgumentException("Le password ne doit pas être null ou vide");
            #endregion

            Login = p_Login;
            Pwd = p_Pwd;
            URL = p_URL;
            AzureID = p_AzureID;
            Redirect = p_Redirect;
            SecretKey = p_SecretKey;
        }
        #endregion

        #region Methode
        //Connection au CRM
        public void ConnectToCRM(DisplayErr Err)
         {
            //Décla de la Config
            Configuration config = null;

            //Appel la config

            config = new FileConfiguration(Login, Pwd, URL, AzureID, Redirect, SecretKey);


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

            //Récup Version API
            Task.WaitAll(Task.Run(async () => await RunAsyncApiVersion(Err)));
        }

        //Appel methodes Asynk pour récup la version
        public async Task RunAsyncApiVersion(DisplayErr Err)
        {
            try
            {
                await ReqApiVersion(GetHTTPClient());
            }
            catch (Exception ex)
            {
                Err.DisplayException(ex);
            }
        }

        //Ferme connexion
        public void Déconnecte()
        {
            httpClient.Dispose();
        }
        #endregion

    }
}
