using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
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

        [JsonProperty(PropertyName = "ioc_acronymfr")]
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

        [JsonProperty(PropertyName = "ioc_postaladdressat")]
        public string m_AddressAt { get; set; }

        [JsonProperty(PropertyName = "ioc_postaladdressstreetcomplement")]
        public string m_AddressComplement { get; set; }

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

        //Retourne String pour requete
        public string GetFederationString()
        {
            string MaChaine = "";

            //Forme la chaine
            foreach (var Fede in GetFederation)
            {
                MaChaine += "('" +
                    Fede.m_Initials + "','" +
                    Fede.m_WebSite + "','" +
                    Utile.SubDate(Fede.m_FoundationDate) + "','" +
                    Utile.ConvertStringSQL(Fede.m_NameFR) + "','" +
                    Utile.ConvertStringSQL(Fede.m_Name) + "','" +
                    Fede.m_Telephone + "','" +
                    Fede.m_Mail + "','" +
                    Fede.m_AddressComplement + "','" +
                    Fede.m_AdressStreet + "','" +
                    Fede.m_AddressBuilding + "','" +
                    Fede.m_AdressCity + "','" +
                    Fede.m_AdressPostalCode + "','" +
                    Fede.m_AdressState + "','" +
                    Fede.m_AdressStateInitial + "','" +
                    Fede.m_AddressAt
                    + "'),";
            }

            //Supp la derniere virgule
            MaChaine = MaChaine.Substring(0, MaChaine.Length - 1);

            //return Chaine
            return MaChaine;
        }

        //Forme la date en année
        public string SubDate(string DateComplet)
        {
            if (string.IsNullOrEmpty(DateComplet))
            {
                return "";
            }
            else
            {
                return DateComplet.Substring(0, 4);
            }
        }
    }


    public class ReqFederation
    {
        private HttpRequestMessage RetrieveGetRequest;
        private HttpResponseMessage RetrieveGetReponse;

        //Récup les contacts
        public async Task<ListFederation> GetFederation(ConnectionCRM Co, string sRequete)
        {
            // Requete des fédérations
            RetrieveGetRequest = new HttpRequestMessage(HttpMethod.Get, Co.getVersionAPI() + sRequete);

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

        public ListFederation GetFederationLocal()
        {

            string MesFede = File.ReadAllText(@"C:\Users\adamerval\source\repos\CIO\CIO Solution\Tool.CIO\Fede.json", Encoding.UTF8);

            //Translate a Content of Request Response to Json Object
            JObject ReponseContacts = JsonConvert.DeserializeObject<JObject>(MesFede);

            // Store values choise in ListFederation with JsonProperty in a member value of contacts
            ListFederation result = JsonConvert.DeserializeObject<ListFederation>(ReponseContacts.ToString());

            //Retourne la liste
            return (result);
        }
    }

}
