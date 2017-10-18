using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GetDataFromBank.Services
{

   // using Transactions = GetDataFromBank.Services.Transactions;
    using GetDataFromBank.Services;

    public class TransactionsParser<T> : Parser<Transactions>
    {
       private String fqcn = typeof(TransactionsParser<T>).FullName;
       //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        
        public Transactions parseJSON(string json)
        {
          //  [JsonProperty(NullValueHandling = NullValueHandling.Ignore)];
            Transactions tr = new Transactions();
            if (json != "{}")
            {
                json = json.Substring(json.IndexOf("["));
                json = json.Substring(0, json.LastIndexOf("]") + 1);
                List<Transaction> transaction = JsonConvert.DeserializeObject<List<Transaction>>(json);//deserializing json object for transaction.
                tr.transaction = transaction;
            }
       
       
       return tr;
        }


       
    }
}