using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GetDataFromBank.Services
{
    //using AssetSummaries = GetDataFromBank.Services.AssetSummaries;
    using GetDataFromBank.Services;
    public class AssetSummariesParser<T> : Parser<AssetSummaries>
    {
        public string fqcn = typeof(AssetSummariesParser<T>).FullName;



        public AssetSummaries parseJSON(string json)
        {

            
            AssetSummaries summaries = new AssetSummaries();
            string mn = "parseJSON(" + json + ")";
            // Console.WriteLine(fqcn + " :: " + mn);
            try
            {
                json = json.Substring(json.IndexOf("["));
                json = json.Substring(0, json.LastIndexOf("]") + 1);
                List<AssetSummary> assetSummary = JsonConvert.DeserializeObject<List<AssetSummary>>(json);//deserialing the json object for holding.

                summaries.assetSummary = assetSummary;
            }
            catch (ArgumentOutOfRangeException)
            {
                return summaries;
            }
            return summaries;


        }
    }
}
