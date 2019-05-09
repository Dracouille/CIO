using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.CIO.CRM.Tools
{
    public class Commission
    {
        [JsonProperty(PropertyName = "Members")]
        public string m_Members { get; set; }

        [JsonProperty(PropertyName = "TechnicalId")]
        public string m_TechnicalId { get; set; }

        [JsonProperty(PropertyName = "IDIOCDF")]
        public long m_IDIOCDF { get; set; }

        [JsonProperty(PropertyName = "Name")]
        public string m_Name { get; set; }

        [JsonProperty(PropertyName = "_LNG")]
        public decimal m_LNG { get; set; }

        [JsonProperty(PropertyName = "_EventID")]
        public long m_EventID { get; set; }

        [JsonProperty(PropertyName = "_Type")]
        public string m_Type { get; set; }
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
}
