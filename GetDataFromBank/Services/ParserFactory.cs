using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GetDataFromBank.Services;

namespace GetDataFromBank.Services
{

    public class ParserFactory
    {
       
        public static readonly string fqcn = typeof(ParserFactory).FullName;

        public static object getParser<T>(Type x,string json) 
        {
            
            string mn = "getParser(" + x + ")";
           
            Parser<T> parser=null;
            
            if (x.FullName.Equals("GetDataFromBank.Services.CobrandContext"))
            {
                parser = (CobrandContextParser<T>)new CobrandContextParser<T>() as Parser<T>;
            }
           
            if (x.FullName.Equals("GetDataFromBank.Services.UserContext"))
            {
                parser = (UserContextParser<T>)new UserContextParser<T>() as Parser<T>;
            }
           
            if (x.FullName.Equals("GetDataFromBank.Services.Accounts"))
            {

                parser = (AccountsParser<T>) new AccountsParser<T>() as Parser<T>;
                       
            }
           
            if (x.FullName.Equals("GetDataFromBank.Services.Transactions"))
            {
                parser = (TransactionsParser<T>)new TransactionsParser<T>() as Parser<T>;
            }
           
            if (x.FullName.Equals("GetDataFromBank.Services.Bills"))
            {
                parser = (BillsParser<T>)new BillsParser<T>() as Parser<T>;
            }
          
            if (x.FullName.Equals("GetDataFromBank.Services.Holdings"))
            {
              parser = (HoldingsParser<T>) new HoldingsParser<T>() as Parser<T>;
            }
           
           if (x.FullName.Equals("GetDataFromBank.Services.Providers"))
            {
                parser = (ProvidersParser<T>)new ProvidersParser<T>() as Parser<T>;
            }
           
            if (x.FullName.Equals("GetDataFromBank.Services.Provider"))
            {
                parser = (SiteParser<T>)new SiteParser<T>() as Parser<T>;
            }
            if (x.FullName.Equals("GetDataFromBank.Services.RefreshStatus"))
            {
                parser = (RefreshStatusParser<T>)new RefreshStatusParser<T>() as Parser<T>;
            }
            if (x.FullName.Equals("GetDataFromBank.Services.AssetSummaries"))
            {
                parser = (AssetSummariesParser<T>)new AssetSummariesParser<T>() as Parser<T>;
            }
            if (x.FullName.Equals("GetDataFromBank.Services.ProviderAccount"))
            {
                parser = (ProviderAccountParser<T>)new ProviderAccountParser<T>() as Parser<T>;
            }

         
            return parser.parseJSON(json);
            
        }

       


        
    }
}