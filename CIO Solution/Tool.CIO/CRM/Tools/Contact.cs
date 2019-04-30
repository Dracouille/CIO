using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.CIO.CRM.Tools
{
    public class Contact
    {
        [JsonProperty(PropertyName = "lastname")]
        public string m_lastname { get; set; }

        [JsonProperty(PropertyName = "firstname")]
        public string m_firstname { get; set; }

        [JsonProperty(PropertyName = "jobtitle")]
        public string m_jobe { get; set; }
    }

    public class ListContact
    {
        // JsonProperty allows to catch a type of value for storing in Json Object
        [JsonProperty(PropertyName = "@odata.context")] 
        public string title { get; set; }

        // use another object for catch just somes values in a list of feature "LIST" allows to call this member with a foreach loop
        [JsonProperty(PropertyName = "value")]
        public List<Contact> GetPerson { get; set; } 
    }
}
