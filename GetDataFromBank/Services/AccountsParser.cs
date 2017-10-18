using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GetDataFromBank.Services
{

    //using Accounts = GetDataFromBank.Services.Accounts;
    public class AccountsParser<T> : Parser<Accounts>
    {
        private String fqcn = typeof(AccountsParser<T>).FullName;
       

        public Accounts parseJSON(string json) 
        {
            string mn = "parseJSON(" + json + ")";
            Accounts a = new Accounts();
            if (json == "{}")
                return a;
            try
            {
                json = json.Substring(json.IndexOf("["));
                json = json.Substring(0, json.LastIndexOf("]") + 1);
                List<Account> account = JsonConvert.DeserializeObject<List<Account>>(json);//deserializing the values for Account app.

                a.account = account;
            }

            catch(ArgumentOutOfRangeException)
            {
               
            }
            return a;

        }

    }
}