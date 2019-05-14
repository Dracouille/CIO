﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Tool.CIO.CRM.Connect;

namespace Tool.CIO.CRM.Tools
{
    public class NOC
    {
        [JsonProperty(PropertyName = "ioc_foundingdateorg")]
        public string m_CreationDate { get; set; }

        [JsonProperty(PropertyName = "ioc_iocrecognition")]
        public string m_RecognitionDate { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string m_Name { get; set; }

        [JsonProperty(PropertyName = "ioc_entitynamefr")]
        public string m_NameFR { get; set; }

        [JsonProperty(PropertyName = "ioc_postaladdressbuildingname")]
        public string m_AddressBuilding { get; set; }

        [JsonProperty(PropertyName = "ioc_postaladdressstreet")]
        public string m_AdressStreet { get; set; }

        [JsonProperty(PropertyName = "ioc_postaladdresscomplement")]
        public string m_AdressComplement { get; set; }

        [JsonProperty(PropertyName = "ioc_postaladdresspostalcode")]
        public string m_AdressPostalCode { get; set; }

        [JsonProperty(PropertyName = "ioc_postaladdresscity")]
        public string m_AdressCity { get; set; }

        [JsonProperty(PropertyName = "ioc_postaladdressstate")]
        public string m_AdressState { get; set; }

        [JsonProperty(PropertyName = "ioc_postaladdressstateinitial")]
        public string m_AdressStateInitial { get; set; }

        [JsonProperty(PropertyName = "ioc_proftel1")]
        public string m_NOCTelephone { get; set; }

        [JsonProperty(PropertyName = "ioc_fax1")]
        public string m_NOCFax { get; set; }

        [JsonProperty(PropertyName = "ioc_noccode")]
        public string m_NOCCode { get; set; }

        [JsonProperty(PropertyName = "websiteurl")]
        public string m_NOCWebsite { get; set; }

        [JsonProperty(PropertyName = "ioc_institutionalemblem")]
        public string m_Emblem { get; set; }

        //@odata.etag, accountid, _ioc_postaladdresscountryid_value ??
    }

    public class ListNOC
    {
        // JsonProperty allows to catch a type of value for storing in Json Object
        [JsonProperty(PropertyName = "@odata.context")]
        public string title { get; set; }

        // use another object for catch just somes values in a list of feature "LIST" allows to call this member with a foreach loop
        [JsonProperty(PropertyName = "value")]
        public List<NOC> GetNOC { get; set; }
    }

    public class ReqNOC
    {
        private HttpRequestMessage RetrieveGetRequest;
        private HttpResponseMessage RetrieveGetReponse;

        //Récup les contacts
        public async Task<ListNOC> GetNOC(ConnectionCRM Co)
        {
            // Requete des NOC
            RetrieveGetRequest = new HttpRequestMessage(HttpMethod.Get, Co.getVersionAPI() + "accounts?userQuery=6a9f7b9a-5675-e911-a81f-000d3a47cb1d");
            //RetrieveGetRequest = new HttpRequestMessage(HttpMethod.Get, Co.getVersionAPI() + "accounts?$top=3");

            // Attend la reception
            RetrieveGetReponse = await Co.GetHTTPClient().SendAsync(RetrieveGetRequest);

            if (RetrieveGetReponse.IsSuccessStatusCode) // if 200, successfully 
            {
                //Translate a Content of Request Response to Json Object
                JObject ReponseContacts = JsonConvert.DeserializeObject<JObject>(await RetrieveGetReponse.Content.ReadAsStringAsync());

                // Store values choise in ListNOC with JsonProperty in a member value of contacts
                ListNOC result = JsonConvert.DeserializeObject<ListNOC>(ReponseContacts.ToString());

                //Retourne la liste
                return (result);
            }
            else
            {
                //LOG
                Console.WriteLine("Echec Récup NOC : {0}", RetrieveGetReponse.ReasonPhrase);
                throw new CrmHttpResponseException(RetrieveGetReponse.Content);
            }
        }
    }

}
