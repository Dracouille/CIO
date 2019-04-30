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
        public async Task GetContact(HttpClient p_httpClient, string VersionApi)
        {
            // Requete des 3 premiers contacts
            RetrieveGetRequest = new HttpRequestMessage(HttpMethod.Get, VersionApi + "contacts?$top=3");
            
            // wait for send a request
            RetrieveGetReponse = await p_httpClient.SendAsync(RetrieveGetRequest); 

            if (RetrieveGetReponse.IsSuccessStatusCode) // if 200, successfully 
            {
                JObject ReponseContacts = JsonConvert.DeserializeObject<JObject>(await RetrieveGetReponse.Content.ReadAsStringAsync()); //Translate a Content of Request Response to Json Object
                ListContact result = JsonConvert.DeserializeObject<ListContact>(ReponseContacts.ToString()); // store a value choise in Cantact with JsonProperty in a member value of contacts

                foreach (var value in result.GetPerson) // and use this value in member
                {
                    Console.WriteLine("Nom : " + value.m_lastname);
                    Console.WriteLine("Prenom : " + value.m_firstname);
                    Console.WriteLine("Situation : " + value.m_jobe);
                }

                //return (result); 
            }
            else
            {
                Console.WriteLine("Echec Récup Contact : {0}", RetrieveGetReponse.ReasonPhrase);
                throw new CrmHttpResponseException(RetrieveGetReponse.Content);
            }
        }
    }
}
