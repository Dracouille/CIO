using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.CIO.CRM.Tools
{
    public class NOC
    {
        [JsonProperty(PropertyName = "_EVENTID")]
        public long m_EventID { get; set; }

        [JsonProperty(PropertyName = "_TYPE")]
        public string m_Type { get; set; }

        [JsonProperty(PropertyName = "_LNG")]
        public decimal m_LNG { get; set; }

        [JsonProperty(PropertyName = "IDIOCDF")]
        public long m_IDIOCDF { get; set; }

        [JsonProperty(PropertyName = "NOCCode")]
        public string m_NOCCode { get; set; }

        [JsonProperty(PropertyName = "Name")]
        public string m_Name { get; set; }

        [JsonProperty(PropertyName = "NOCAddress")]
        public string m_NOCAddress { get; set; }

        [JsonProperty(PropertyName = "NOCTelephone")]
        public string m_NOCTelephone { get; set; }

        [JsonProperty(PropertyName = "NOCFax")]
        public string m_NOCFax { get; set; }

        [JsonProperty(PropertyName = "NOCEmail")]
        public string m_NOCEmail { get; set; }

        [JsonProperty(PropertyName = "IDENTITYCARDEMAIL_LINKURL")]
        public string m_IDENTITYCARDEMAIL_LINKURL { get; set; }

        [JsonProperty(PropertyName = "IDENTITYCARDEMAIL_LINKTOOLTIP")]
        public string m_IDENTITYCARDEMAIL_LINKTOOLTIP { get; set; }

        [JsonProperty(PropertyName = "NOCOfficialName")]
        public string m_NOCOfficialName { get; set; }

        [JsonProperty(PropertyName = "NOCWebsite")]
        public string m_NOCWebsite { get; set; }

        [JsonProperty(PropertyName = "IDENTITYCARDWEBSITE_LINKTOOLTIP")]
        public string m_IDENTITYCARDWEBSITE_LINKTOOLTIP { get; set; }

        [JsonProperty(PropertyName = "CreationDate")]
        public DateTime m_CreationDate { get; set; }

        [JsonProperty(PropertyName = "RecognitionDate")]
        public string m_RecognitionDate { get; set; }

        [JsonProperty(PropertyName = "NOCFirstRepresentativeTitle")]
        public string m_NOCFirstRepresentativeTitle { get; set; }

        [JsonProperty(PropertyName = "NOCSecondRepresentativeTitle")]
        public string m_NOCSecondRepresentativeTitle { get; set; }

        [JsonProperty(PropertyName = "NOCFirstRepresentativePerson")]
        public string m_NOCFirstRepresentativePerson { get; set; }

        [JsonProperty(PropertyName = "NOCSecondRepresentativePerson")]
        public string m_NOCSecondRepresentativePerson { get; set; }

        [JsonProperty(PropertyName = "IDENTITYCARDCOUNTRYWEBSITEICONIMAGEURL")]
        public string m_IDENTITYCARDCOUNTRYWEBSITEICONIMAGEURL { get; set; }

        [JsonProperty(PropertyName = "IDENTITYCARDCOUNTRYWEBSITEICONALTTEXT")]
        public string m_IDENTITYCARDCOUNTRYWEBSITEICONALTTEXT { get; set; }

        [JsonProperty(PropertyName = "IDENTITYCARDCOUNTRYWEBSITEICONIMAGETITLE")]
        public string m_IDENTITYCARDCOUNTRYWEBSITEICONIMAGETITLE { get; set; }

        [JsonProperty(PropertyName = "IDENTITYCARDCOUNTRYWEBSITELINKLINKTEXT")]
        public string m_IDENTITYCARDCOUNTRYWEBSITELINKLINKTEXT { get; set; }

        [JsonProperty(PropertyName = "IDENTITYCARDCOUNTRYWEBSITELINKLINKURL")]
        public string m_IDENTITYCARDCOUNTRYWEBSITELINKLINKURL { get; set; }

        [JsonProperty(PropertyName = "IDENTITYCARDCOUNTRYWEBSITELINKLINKTOOLTIP")]
        public string m_IDENTITYCARDCOUNTRYWEBSITELINKLINKTOOLTIP { get; set; }

        [JsonProperty(PropertyName = "__NOC")]
        public string m_NOC { get; set; }

        [JsonProperty(PropertyName = "OLYMPICGAMES")]
        public string m_OLYMPICGAMES { get; set; }

        [JsonProperty(PropertyName = "PAGEID")]
        public ulong m_PAGEID { get; set; }

        [JsonProperty(PropertyName = "PAGEGUID")]
        public int m_PAGEGUID { get; set; }
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
}
