using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GetDataFromBank.Services;
using Newtonsoft.Json;

namespace GetDataFromBank.Services
{
   

        class ProviderAccountParser<T> : Parser<ProviderAccount>
    {
            public ProviderAccount parseJSON(String json) {
                ProviderAccount providerAccount = JsonConvert.DeserializeObject<ProviderAccount>(json);//deserializing the values for Account app.
                // Accounts a = new Accounts();
                // a.account = account;
                //return a;
                return providerAccount;
            }

    }
}
