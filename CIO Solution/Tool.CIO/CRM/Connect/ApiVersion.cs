using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tools.CIO.Tools;

namespace Tool.CIO.CRM.Connect
{
    public class ApiVersion
    {

        //Start with base version and update with actual version later.
        private Version webAPIVersion = new Version(8, 0);

        public async Task ReqApiVersion(HttpClient httpClient)
        {
            HttpRequestMessage RetrieveVersionRequest = new HttpRequestMessage(HttpMethod.Get, getVersionAPI() + "RetrieveVersion");

            HttpResponseMessage RetrieveVersionResponse = await httpClient.SendAsync(RetrieveVersionRequest);
            if (RetrieveVersionResponse.StatusCode == HttpStatusCode.OK)  //200
            {
                JObject RetrievedVersion = JsonConvert.DeserializeObject<JObject>(await RetrieveVersionResponse.Content.ReadAsStringAsync());
                //Capture the actual version available in this organization
                webAPIVersion = Version.Parse((string)RetrievedVersion.GetValue("Version"));
            }
            else
            {
                System.Console.WriteLine("Failed to retrieve the version for reason: {0}",
                    RetrieveVersionResponse.ReasonPhrase);
                throw new CrmHttpResponseException(RetrieveVersionResponse.Content);
            }
        }

        public string getVersionAPI()
        {
            return string.Format("v{0}/", webAPIVersion.ToString(2));
        }

    }
}
