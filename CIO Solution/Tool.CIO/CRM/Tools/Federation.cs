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
    public class Federation
    {
        [JsonProperty(PropertyName = "name")]
        public string m_Name { get; set; }

        [JsonProperty(PropertyName = "ioc_acronymen")]
        public string m_Initials { get; set; }

        [JsonProperty(PropertyName = "ioc_foundingdateorg")]
        public string m_FoundationDate { get; set; }

        [JsonProperty(PropertyName = "ioc_noaffiliatednif")]
        public int m_NbOfAffiliates { get; set; }

        [JsonProperty(PropertyName = "websiteurl")]
        public string m_WebSite { get; set; }

        [JsonProperty(PropertyName = "ioc_postaladdressbuildingname")]
        public string m_AddressBuilding { get; set; }

        [JsonProperty(PropertyName = "ioc_postaladdressstreet")]
        public string m_AdressStreet { get; set; }

        [JsonProperty(PropertyName = "ioc_postaladdresspostalcode")]
        public string m_AdressPostalCode { get; set; }

        [JsonProperty(PropertyName = "ioc_postaladdresscity")]
        public string m_AdressCity { get; set; }

        [JsonProperty(PropertyName = "ioc_postaladdressstate")]
        public string m_AdressState { get; set; }

        [JsonProperty(PropertyName = "ioc_postaladdressstateinitial")]
        public string m_AdressStateInitial { get; set; }

        [JsonProperty(PropertyName = "ioc_entitynamefr")]
        public string m_NameFR { get; set; }

        [JsonProperty(PropertyName = "ioc_proftel1")]
        public string m_Telephone { get; set; }

        [JsonProperty(PropertyName = "emailaddress1")]
        public string m_Mail { get; set; }

        //AccountID, _ioc_postaladdresscountryid_value, _ioc_sportid_value, @odata.etag ??
    }

    public class ListFederation
    {
        // JsonProperty allows to catch a type of value for storing in Json Object
        [JsonProperty(PropertyName = "@odata.context")]
        public string title { get; set; }

        // use another object for catch just somes values in a list of feature "LIST" allows to call this member with a foreach loop
        [JsonProperty(PropertyName = "value")]
        public List<Federation> GetFederation { get; set; }
    }
    

    public class ReqFederation
    {
        private HttpRequestMessage RetrieveGetRequest;
        private HttpResponseMessage RetrieveGetReponse;

        //Récup les contacts
        public async Task<ListFederation> GetFederation(ConnectionCRM Co)
        {
            // Requete des fédérations
            RetrieveGetRequest = new HttpRequestMessage(HttpMethod.Get, Co.getVersionAPI() + "accounts?userQuery=48cd60b8-5875-e911-a81f-000d3a47cb1d");

            // Attend la reception
            RetrieveGetReponse = await Co.GetHTTPClient().SendAsync(RetrieveGetRequest);

            if (RetrieveGetReponse.IsSuccessStatusCode) // if 200, successfully 
            {
                //Translate a Content of Request Response to Json Object
                JObject ReponseContacts = JsonConvert.DeserializeObject<JObject>(await RetrieveGetReponse.Content.ReadAsStringAsync());

                // Store values choise in ListFederation with JsonProperty in a member value of contacts
                ListFederation result = JsonConvert.DeserializeObject<ListFederation>(ReponseContacts.ToString());

                //Retourne la liste
                return (result);
            }
            else
            {
                //LOG
                Console.WriteLine("Echec Récup Fédérations : {0}", RetrieveGetReponse.ReasonPhrase);
                throw new CrmHttpResponseException(RetrieveGetReponse.Content);
            }
        }
    }
}
