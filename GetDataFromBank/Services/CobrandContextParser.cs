using Newtonsoft.Json;

namespace GetDataFromBank.Services
{

    //using CobrandContext = GetDataFromBank.Services.CobrandContext;

    public class CobrandContextParser<T> : Parser<CobrandContext>
    {
        

        public string fqcn = typeof(CobrandContextParser<T>).FullName;

       
        public CobrandContext parseJSON(string json)
     
        {
           
            string mn = "parseJSON(" + json + ")";
           // Console.WriteLine(fqcn + " :: " + mn);
             
            CobrandContext cobrandcntxt = JsonConvert.DeserializeObject<CobrandContext>(json);
            return cobrandcntxt;


        }

    }
}
