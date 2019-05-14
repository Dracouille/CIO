using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tool.CIO.CRM.Connect;

namespace Tool.CIO.CRM.Tools
{
    public class ReqAPI
    {
        private HttpRequestMessage RetrieveGetRequest;
        private HttpResponseMessage RetrieveGetReponse;

        //Récup les contacts
        public async Task<ListContact> GetContact(ConnectionCRM Co)
        {
            // Requete des 3 premiers contacts
            RetrieveGetRequest = new HttpRequestMessage(HttpMethod.Get, Co.getVersionAPI() + "contacts?$top=3");
            
            // Attend la reception
            RetrieveGetReponse = await Co.GetHTTPClient().SendAsync(RetrieveGetRequest); 

            if (RetrieveGetReponse.IsSuccessStatusCode) // if 200, successfully 
            {
                //Translate a Content of Request Response to Json Object
                JObject ReponseContacts = JsonConvert.DeserializeObject<JObject>(await RetrieveGetReponse.Content.ReadAsStringAsync());
                
                // Store values choise in ListContact with JsonProperty in a member value of contacts
                ListContact result = JsonConvert.DeserializeObject<ListContact>(ReponseContacts.ToString()); 
                
                //Retourne la liste
                return (result); 
            }
            else
            {
                Console.WriteLine("Echec Récup Contact : {0}", RetrieveGetReponse.ReasonPhrase);
                throw new CrmHttpResponseException(RetrieveGetReponse.Content);
            }
        }

        


    }
}
