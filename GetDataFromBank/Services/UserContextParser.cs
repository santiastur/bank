using Newtonsoft.Json;

namespace GetDataFromBank.Services
{

    //using UserContext = GetDataFromBank.Services.UserContext;



    public class UserContextParser<T> : Parser<UserContext>
    {



        public string fqcn = typeof(UserContextParser<T>).FullName;
        
        public UserContext parseJSON(string json)
    {
        string mn = "parseJSON(" + json + ")";
        //Console.WriteLine(fqcn + " :: " + mn);
        UserContext usercontextt = JsonConvert.DeserializeObject<UserContext>(json);//deserialising json object for UserContext class
        return usercontextt;

    }

       

        
    }
}