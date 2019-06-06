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

        [JsonProperty(PropertyName = "ab_x002e_name")]
        public string m_IOCName { get; set; }

        [JsonProperty(PropertyName = "ioc_entrydate")]
        public string m_DateEntre { get; set; }

        [JsonProperty(PropertyName = "ioc_enddate")]
        public string m_DateSortit { get; set; }

        [JsonProperty(PropertyName = "a_63cecba3a0c8e811a950000d3a441525_x002e_firstname")]
        public string m_FirstName { get; set; }

        [JsonProperty(PropertyName = "a_63cecba3a0c8e811a950000d3a441525_x002e_lastname")]
        public string m_LastName { get; set; }

        [JsonProperty(PropertyName = "a_63cecba3a0c8e811a950000d3a441525_x002e_ioc_fullnamefr")]
        public string m_FullNameFR { get; set; }

        [JsonProperty(PropertyName = "a_63cecba3a0c8e811a950000d3a441525_x002e_ioc_fullnameen")]
        public string m_FullNameEN { get; set; }

        [JsonProperty(PropertyName = "a_63cecba3a0c8e811a950000d3a441525_x002e_birthdate")]
        public string m_DateNaissance { get; set; }

        [JsonProperty(PropertyName = "a_63cecba3a0c8e811a950000d3a441525_x002e_ioc_cityofbirth")]
        public string m_BirthCity { get; set; }
    }

    public class ListMember
    {
        // JsonProperty allows to catch a type of value for storing in Json Object
        [JsonProperty(PropertyName = "@odata.context")]
        public string title { get; set; }

        // use another object for catch just somes values in a list of feature "LIST" allows to call this member with a foreach loop
        [JsonProperty(PropertyName = "value")]
        public List<Member> GetMember { get; set; }

        //Retourne String pour requete d'insert
        public string GetMemberString()
        {
            string MaChaine = "";

            //Forme la chaine
            foreach (var Mem in GetMember)
            {
                MaChaine += "('" +
                    Mem.m_LastName.ToUpper() + "','" +
                    Mem.m_FirstName + "','" +
                    Utile.SubDate(Mem.m_DateNaissance) + "','" +
                    Utile.SubDate(Mem.m_DateEntre) + "','" +
                    Utile.SubDate(Mem.m_DateSortit) + "','" +
                    Mem.m_AdressCity + "','" +
                    Utile.ConvertStringSQL(Utile.ExtractCivil(Mem.m_FullNameFR,Mem.m_FirstName)) + "','" +
                    Utile.ConvertStringSQL(Utile.ExtractCivil(Mem.m_FullNameEN,Mem.m_FirstName)) + "','" +
                    Mem.m_BirthCity 
                    + "'),";
            }

            //Supp la derniere virgule
            MaChaine = MaChaine.Substring(0, MaChaine.Length - 1);

            //return Chaine
            return MaChaine;
        }
    }

    public class ReqMember
    {
        private HttpRequestMessage RetrieveGetRequest;
        private HttpResponseMessage RetrieveGetReponse;

        //Récup les contacts
        public async Task<ListMember> GetMember(ConnectionCRM Co, string sRequete)
        {
            // Requete des NOC
            RetrieveGetRequest = new HttpRequestMessage(HttpMethod.Get, Co.getVersionAPI() + sRequete);

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
