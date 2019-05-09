using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.CIO.CRM.Tools
{
    public class Federation
    {
        [JsonProperty(PropertyName = "_EVENTID")]
        public long m_EventID { get; set; }

        [JsonProperty(PropertyName = "_TYPE")]
        public string m_Type { get; set; }

        [JsonProperty(PropertyName = "_LNG")]
        public decimal m_LNG { get; set; }

        [JsonProperty(PropertyName = "IDIOCDF")]
        public long m_IDIOCDF { get; set; }

        [JsonProperty(PropertyName = "Initials")]
        public string m_Initials { get; set; }

        [JsonProperty(PropertyName = "Name")]
        public string m_Name { get; set; }

        [JsonProperty(PropertyName = "IdentityCardAddress")]
        public string m_IdentityCardAddress { get; set; }

        [JsonProperty(PropertyName = "IdentityCardPhone")]
        public string m_IdentityCardPhone { get; set; }

        [JsonProperty(PropertyName = "IdentityCardFax")]
        public string m_IdentityCardFax { get; set; }

        [JsonProperty(PropertyName = "IDENTITYCARDEMAIL_LINKURL")]
        public string m_IDENTITYCARDEMAIL_LINKURL { get; set; }

        [JsonProperty(PropertyName = "IdentityCardEmail")]
        public string m_IdentityCardEmail { get; set; }

        [JsonProperty(PropertyName = "IDENTITYCARDEMAIL_LINKTOOLTIP")]
        public string m_IDENTITYCARDEMAIL_LINKTOOLTIP { get; set; }

        [JsonProperty(PropertyName = "IDENTITYCARDWEBSITE_LINKTEXT")]
        public string m_IDENTITYCARDWEBSITE_LINKTEXT { get; set; }

        [JsonProperty(PropertyName = "IdentityCardWebsite")]
        public string m_IdentityCardWebsite { get; set; }

        [JsonProperty(PropertyName = "IDENTITYCARDWEBSITE_LINKTOOLTIP")]
        public string m_IDENTITYCARDWEBSITE_LINKTOOLTIP { get; set; }

        [JsonProperty(PropertyName = "FoundationDate")]
        public string m_FoundationDate { get; set; }

        [JsonProperty(PropertyName = "NbOfAffiliates")]
        public int m_NbOfAffiliates { get; set; }

        [JsonProperty(PropertyName = "FEDERATIONLOGOIMAGE_IMAGETITLE")]
        public string m_FEDERATIONLOGOIMAGE_IMAGETITLE { get; set; }

        [JsonProperty(PropertyName = "FEDERATIONLOGOIMAGE_ALTTEXT")]
        public string m_FEDERATIONLOGOIMAGE_ALTTEXT { get; set; }

        [JsonProperty(PropertyName = "LogoUrl")]
        public string m_LogoUrl { get; set; }

        [JsonProperty(PropertyName = "SportCode")]
        public string m_SportCode { get; set; }

        [JsonProperty(PropertyName = "SPORTNAME")]
        public string m_SPORTNAME { get; set; }

        [JsonProperty(PropertyName = "__SPO")]
        public string m_SPO { get; set; }

        [JsonProperty(PropertyName = "PAGEID")]
        public ulong m_PAGEID { get; set; }

        [JsonProperty(PropertyName = "PAGEGUID")]
        public int m_PAGEGUID { get; set; }
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
}
