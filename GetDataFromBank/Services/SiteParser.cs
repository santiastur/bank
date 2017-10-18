using System.Collections.Generic;
using Newtonsoft.Json;

namespace GetDataFromBank.Services
{
    //using Provider = GetDataFromBank.Services.Provider;
    using GetDataFromBank.Services;
    public class SiteParser<T> : Parser<Provider>
    {
        public string fqcn = typeof(SiteParser<T>).FullName;
        public Provider parseJSON(string json)
        {
            string mn = "parseJSON(" + json + ")";
            //Console.WriteLine(fqcn + " :: " + mn);
            json = json.Substring(json.IndexOf("["));
            json = json.Substring(0, json.LastIndexOf("]") + 1);
            List<Site> prov = JsonConvert.DeserializeObject<List<Site>>(json);//deserialising the value for site class
           // Provider p = new Provider();
           // p.provider = prov;
            return null;
            

        }
    }
}