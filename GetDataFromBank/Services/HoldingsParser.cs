using System;
using System.Collections.Generic;
using Newtonsoft.Json;


namespace GetDataFromBank.Services
{

   // using Holdings = GetDataFromBank.Services.Holdings;
    using GetDataFromBank.Services;

    public class HoldingsParser<T> : Parser<Holdings>
    {
        public string fqcn = typeof(HoldingsParser<T>).FullName;


    
        public  Holdings parseJSON(string json)
       
        {

            Holdings h = new Holdings();
            string mn = "parseJSON(" + json + ")";
           // Console.WriteLine(fqcn + " :: " + mn);
            try
            {
                json = json.Substring(json.IndexOf("["));
                json = json.Substring(0, json.LastIndexOf("]") + 1);
                List<Holding> holding = JsonConvert.DeserializeObject<List<Holding>>(json);//deserialing the json object for holding.

                h.holding = holding;
               
            }
            catch(ArgumentOutOfRangeException){
                return h;
            }
            return h;
        
            
        }
    }
}