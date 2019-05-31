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
    public class Member
    {
        [JsonProperty(PropertyName = "ioc_postaladdresscity")]
        public string m_AdressCity { get; set; }

        [JsonProperty(PropertyName = "ioc_postaladdresspostalcode")]
        public string m_AdressPostalCode { get; set; }

        [JsonProperty(PropertyName = "ioc_functionfreefield")]
        public string m_FunctionFree { get; set; }

        [JsonProperty(PropertyName = "a_6a4728cfc070e911a81c000d3a47c8f5_x002e_firstname")]
        public string m_FirstName { get; set; }

        [JsonProperty(PropertyName = "a_6a4728cfc070e911a81c000d3a47c8f5_x002e_ioc_fullnamefr")]
        public string m_FullNameFR{ get; set; }

        [JsonProperty(PropertyName = "a_6a4728cfc070e911a81c000d3a47c8f5_x002e_ioc_biographyfr")]
        public string m_BiographieFR { get; set; }

        [JsonProperty(PropertyName = "a_6a4728cfc070e911a81c000d3a47c8f5_x002e_lastname")]
        public string m_LastName { get; set; }

        [JsonProperty(PropertyName = "a_6a4728cfc070e911a81c000d3a47c8f5_x002e_ioc_biographyen")]
        public string m_BiographieEN { get; set; }

        [JsonProperty(PropertyName = "a_6a4728cfc070e911a81c000d3a47c8f5_x002e_ioc_fullnameen")]
        public string m_FullNameEN { get; set; }

        [JsonProperty(PropertyName = "func_x002e_ioc_name")]
        public string m_IOCName { get; set; }

        [JsonProperty(PropertyName = "a_6a4728cfc070e911a81c000d3a47c8f5_x002e_birthdate")]
        public string m_DateNaissance { get; set; }
    }

    public class ListMember
    {
        // JsonProperty allows to catch a type of value for storing in Json Object
        [JsonProperty(PropertyName = "@odata.context")]
        public string title { get; set; }

        // use another object for catch just somes values in a list of feature "LIST" allows to call this member with a foreach loop
        [JsonProperty(PropertyName = "value")]
        public List<Member> GetMember { get; set; }
    }

    public class ReqMember
    {
        private HttpRequestMessage RetrieveGetRequest;
        private HttpResponseMessage RetrieveGetReponse;

        //Récup les contacts
        public async Task<ListMember> GetMember(ConnectionCRM Co)
        {
            // Requete des NOC
            RetrieveGetRequest = new HttpRequestMessage(HttpMethod.Get, Co.getVersionAPI() + "ioc_roles?userQuery=2194bf98-7975-e911-a81f-000d3a47c97c");

            // Attend la reception
            RetrieveGetReponse = await Co.GetHTTPClient().SendAsync(RetrieveGetRequest);

            if (RetrieveGetReponse.IsSuccessStatusCode) // if 200, successfully 
            {
                //Translate a Content of Request Response to Json Object
                JObject ReponseContacts = JsonConvert.DeserializeObject<JObject>(await RetrieveGetReponse.Content.ReadAsStringAsync());

                // Store values choise in ListMember with JsonProperty in a member value of contacts
                ListMember result = JsonConvert.DeserializeObject<ListMember>(ReponseContacts.ToString());

                //Retourne la liste
                return (result);
            }
            else
            {
                //LOG
                Console.WriteLine("Echec Récup Members : {0}", RetrieveGetReponse.ReasonPhrase);
                throw new CrmHttpResponseException(RetrieveGetReponse.Content);
            }
        }
    }
}
