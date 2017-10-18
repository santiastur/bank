using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
using GetDataFromBank.Services;
using System.Reflection;

namespace GetDataFromBank.Services
{


    public class GSONParser
    {
        
        public static readonly string fqcn = typeof(GSONParser).FullName;

      
        public static object handleJson(string json, Type T)
        {
           
            string mn = "handleJson(" + json + ", " + T + " )";

            MethodInfo method = typeof(ParserFactory).GetMethod("getParser");//Retrieving class type
            MethodInfo generic = method.MakeGenericMethod(T);// coverting to generic
            object[] obj = { T, json };//Converting to json object
            var x = generic.Invoke(null, obj);//invoke the object
            return x;

        }


       

    }
}