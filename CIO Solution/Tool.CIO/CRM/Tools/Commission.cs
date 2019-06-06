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

    public class Commission
    {
        [JsonProperty(PropertyName = "ioc_postaladdresscity")]
        public string m_AdressCity { get; set; }

        [JsonProperty(PropertyName = "ioc_email1")]
        public string m_Mail1 { get; set; }

        [JsonProperty(PropertyName = "a_6a4728cfc070e911a81c000d3a47c8f5_x002e_ioc_fullnamefr")]
        public string m_NameFr { get; set; }

        [JsonProperty(PropertyName = "a_6a4728cfc070e911a81c000d3a47c8f5_x002e_ioc_fullnameen")]
        public string m_Name { get; set; }

        [JsonProperty(PropertyName = "aa_x002e_ioc_entitynamefr")]
        public string m_EntityName { get; set; }

        [JsonProperty(PropertyName = "a_6a4728cfc070e911a81c000d3a47c8f5_x002e_fullname")]
        public string m_FullName { get; set; }

        [JsonProperty(PropertyName = "ioc_functionfreefield")]
        public string m_Function { get; set; }

        [JsonProperty(PropertyName = "aa_x002e_name")]
        public string m_NameCommission { get; set; }

        //@odata.etag, ioc_roleid, _ioc_personid_value, _ownerid_value, _ioc_accountid_value, _ioc_postaladdresscountryid_value ??
    }

    public class ListCommission
    {
        // JsonProperty allows to catch a type of value for storing in Json Object
        [JsonProperty(PropertyName = "@odata.context")]
        public string title { get; set; }

        // use another object for catch just somes values in a list of feature "LIST" allows to call this member with a foreach loop
        [JsonProperty(PropertyName = "value")]
        public List<Commission> GetCommission { get; set; }
    }

    public class ReqCommission
    {
        private HttpRequestMessage RetrieveGetRequest;
        private HttpResponseMessage RetrieveGetReponse;

        //Récup les contacts
        public async Task<ListCommission> GetCommission(ConnectionCRM Co, string sRequete)
        {
            // Requete des fédérations
            //RetrieveGetRequest = new HttpRequestMessage(HttpMethod.Get, Co.getVersionAPI() + "ioc_roles?userQuery=5ed70282-5a75-e911-a81f-000d3a47cb1d");
            RetrieveGetRequest = new HttpRequestMessage(HttpMethod.Get, Co.getVersionAPI() + sRequete);

            // Attend la reception
            RetrieveGetReponse = await Co.GetHTTPClient().SendAsync(RetrieveGetRequest);

            if (RetrieveGetReponse.IsSuccessStatusCode) // if 200, successfully 
            {
                //Translate a Content of Request Response to Json Object
                JObject ReponseContacts = JsonConvert.DeserializeObject<JObject>(await RetrieveGetReponse.Content.ReadAsStringAsync());

                // Store values choise in ListCommission with JsonProperty in a member value of contacts
                ListCommission result = JsonConvert.DeserializeObject<ListCommission>(ReponseContacts.ToString());

                //Retourne la liste
                return (result);
            }
            else
            {
                //LOG
                Console.WriteLine("Echec Récup Commissions : {0}", RetrieveGetReponse.ReasonPhrase);
                throw new CrmHttpResponseException(RetrieveGetReponse.Content);
            }
        }
    }
}
