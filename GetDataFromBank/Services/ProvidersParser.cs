using System;
using Newtonsoft.Json;


namespace GetDataFromBank.Services
{
    //using Providers = GetDataFromBank.Services.Providers;
    using GetDataFromBank.Services;

    public class ProvidersParser<T> : Parser<Providers>
    {
        public string fqcn = typeof(ProvidersParser<T>).FullName;
      
        public Providers parseJSON(string json)
        {
            string mn = "parseJSON(" + json + ")";
            Providers pr = new Providers();
            try
            {
                //Console.WriteLine(fqcn + " :: " + mn);
                json = json.Substring(json.IndexOf("["));
                json = json.Substring(0, json.LastIndexOf("]") + 1);
                Provider[] providr = JsonConvert.DeserializeObject<Provider[]>(json);

                pr.provider = providr;
            }
            catch (ArgumentOutOfRangeException) {
                return pr;
            }
           //return null;   
           return pr;
           
        }
    
    }
}