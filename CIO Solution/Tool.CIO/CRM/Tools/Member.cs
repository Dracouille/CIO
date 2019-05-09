using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.CIO.CRM.Tools
{
    public class Member
    {
        [JsonProperty(PropertyName = "_EVENTID")]
        public long m_EventID { get; set; }

        [JsonProperty(PropertyName = "_TYPE")]
        public string m_Type { get; set; }

        [JsonProperty(PropertyName = "_LNG")]
        public decimal m_LNG { get; set; }

        [JsonProperty(PropertyName = "IDIOCDF")]
        public long m_IDIOCDF { get; set; }

        [JsonProperty(PropertyName = "RccId")]
        public int m_RccId { get; set; }

        [JsonProperty(PropertyName = "PAGETITLE")]
        public string m_PAGETITLE { get; set; }

        [JsonProperty(PropertyName = "Title")]
        public string m_Title { get; set; }

        [JsonProperty(PropertyName = "Name")]
        public string m_Name { get; set; }

        [JsonProperty(PropertyName = "lastname")]
        public string m_lastname { get; set; }

        [JsonProperty(PropertyName = "MEMBERPHOTO_IMAGEURL")]
        public string m_MEMBERPHOTO_IMAGEURL { get; set; }

        [JsonProperty(PropertyName = "MEMBERPHOTO_ALTTEXT")]
        public string m_MEMBERPHOTO_ALTTEXT { get; set; }

        [JsonProperty(PropertyName = "MEMBERPHOTO_IMAGETITLE")]
        public string m_MEMBERPHOTO_IMAGETITLE { get; set; }

        [JsonProperty(PropertyName = "EntryYear")]
        public uint m_EntryYear { get; set; }

        [JsonProperty(PropertyName = "ExitYear")]
        public uint m_ExitYear { get; set; }

        [JsonProperty(PropertyName = "NOCInitials")]
        public string m_NOCInitials { get; set; }

        [JsonProperty(PropertyName = "MEMBERCOUNTRY")]
        public string m_MEMBERCOUNTRY { get; set; }

        [JsonProperty(PropertyName = "IsAnExecutiveBoardMember")]
        public string m_IsAnExecutiveBoardMember { get; set; }

        [JsonProperty(PropertyName = "ISIOCMEMBER")]
        public int m_ISIOCMEMBER { get; set; }

        [JsonProperty(PropertyName = "IsAnActiveIOCMember")]
        public string m_IsAnActiveIOCMember { get; set; }

        [JsonProperty(PropertyName = "IsAnIOCMember")]
        public string m_IsAnIOCMember { get; set; }

        [JsonProperty(PropertyName = "IsAHonoraryIOCMember")]
        public string m_IsAHonoraryIOCMember { get; set; }

        [JsonProperty(PropertyName = "IsAHonorIOCMember")]
        public string m_IsAHonorIOCMember { get; set; }

        [JsonProperty(PropertyName = "IsAMedallist")]
        public string m_IsAMedallist { get; set; }

        [JsonProperty(PropertyName = "IsAnOlympian")]
        public string m_IsAnOlympian { get; set; }

        [JsonProperty(PropertyName = "PARTICIPATIONYEARS")]
        public string m_PARTICIPATIONYEARS { get; set; }

        [JsonProperty(PropertyName = "ProtocolOrder")]
        public byte m_ProtocolOrder { get; set; }

        [JsonProperty(PropertyName = "Birthdate")]
        public DateTime m_Birthdate { get; set; }

        [JsonProperty(PropertyName = "BirthLocation")]
        public string m_BirthLocation { get; set; }

        [JsonProperty(PropertyName = "__NOC")]
        public string m_NOC { get; set; }

        [JsonProperty(PropertyName = "Description")]
        public string m_Description { get; set; }

        [JsonProperty(PropertyName = "PAGEID")]
        public ulong m_PAGEID { get; set; }

        [JsonProperty(PropertyName = "PAGEGUID")]
        public int m_PAGEGUID { get; set; }
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
}
