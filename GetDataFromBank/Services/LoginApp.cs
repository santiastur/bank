using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace GetDataFromBank.Services
{

   
   
    public class LoginApp
    { 


        public static string usession { get; set; }
    public static string cbsession { get; set; }
    private static readonly string fqcn = typeof(LoginApp).FullName;
    public static IDictionary<string, string> loginTokens = new Dictionary<string, string>();
    public static string localURLVer1 = ConfigurationManager.AppSettings["url"];//"http://localhost:8080/ysl/yodlee/";

    public static int registerUser()
    {
        
        string userName = ConfigurationManager.AppSettings["cobUserName"];
        string password = ConfigurationManager.AppSettings["cobPassword"];

        string registerJson = ConfigurationManager.AppSettings["regJson"];
        string registerUrl = localURLVer1 + "/user/register";
        string cobses = loginTokens["cobSession"];//cobrand session id
        List<string> headers = new List<string>();
        headers.Add("Authorization:{cobSession= " + cobses + "}");//passing cobrand session id.
        string jsonResponse = HTTP.doPostUser(registerUrl, headers, null, registerJson);
        if (jsonResponse == null)
        {
            Console.WriteLine("Wrong cobrand username/password");
            return 0;

        }
        else
        {
            CobrandContext coBrand = (CobrandContext)GSONParser.handleJson(jsonResponse, typeof(CobrandContext));
            Console.WriteLine("--- Cobrand Session Id ---");
            Console.WriteLine(coBrand.session.cobSession);
            Console.WriteLine("--------------------------");
            loginTokens["cobSession"] = coBrand.session.cobSession;
            cbsession = coBrand.session.cobSession;
            return 1;
        }
    }

    public static int doCoBrandLogin()
    {
        
        string coBrandUserName = ConfigurationManager.AppSettings["cobUserName"]; 
        string coBrandPassword = ConfigurationManager.AppSettings["cobPassword"]; 
        //string mn = "doCoBrandLogin(coBrandUserName " + coBrandUserName + ", coBrandPassword " + coBrandPassword + " )";
        //string requestBody = "cobrandLogin=" + coBrandUserName + "&cobrandPassword=" + coBrandPassword;
        // string requestBody = "{\"cobrand\":{\"cobrandLogin\":\"" +coBrandUserName + "\",\"cobrandPassword\":\"" + coBrandPassword+"\"}";
        string requestBody = "{\"cobrand\": {\"cobrandLogin\":\"" + coBrandUserName + "\",\"cobrandPassword\":\"" + coBrandPassword + "\"}}";
        string coBrandLoginURL = localURLVer1 + "cobrand/login";
        string jsonResponse = HTTP.doPostUser(coBrandLoginURL, null, requestBody, null);
        if (jsonResponse == null)
        {
         
            return 0;

        }
        else
        {
            CobrandContext coBrand = (CobrandContext)GSONParser.handleJson(jsonResponse, typeof(CobrandContext));
            loginTokens["cobSession"] = coBrand.session.cobSession;
            cbsession = coBrand.session.cobSession;
            return 1;
        }
    }


    public static int doMemberLogin()
    {
        
        string userName = ConfigurationManager.AppSettings["cobUserNameQA"]; 
        string userPassword = ConfigurationManager.AppSettings["cobPasswordQA"];
               
        string requestBody = "{\"user\": {\"loginName\":\"" + userName + "\",\"password\":\"" + userPassword + "\"}}";

        string userLoginURL = localURLVer1 + "user/login";
        string cobses = loginTokens["cobSession"];
        List<string> headers = new List<string>();
        headers.Add("Authorization:{cobSession= " + cobses + "}");//passing cobrand session id.
        string jsonResponse = HTTP.doPostUser(userLoginURL, headers, requestBody, null);
        if (jsonResponse == null)
        {
            
            return 0;

        }
        else
        {
            UserContext member = (UserContext)GSONParser.handleJson(jsonResponse, typeof(UserContext));
            loginTokens["userSession"] = member.user.session.userSession;
            usession = loginTokens["userSession"];
            return 1;
        }


    }



    public static void doLogin(string coBrandUserName, string coBrandPassword, string userName, string userPassword)
    {

        //doCoBrandLogin(coBrandUserName, coBrandPassword);
        doMemberLogin();
    }
    public static void Main()
    {
        
        int MAX_LOGINS = 1, memberSuccess, cobrandSuccess;
        bool auth = false;
        int count = 0;
        do
        {

            cobrandSuccess = LoginApp.doCoBrandLogin();
            while (cobrandSuccess == 0)
            {
                Console.WriteLine("Try Again");
                LoginApp.Main();

            }
            //                    Console.WriteLine("Select the apps to run:");
            //                    Console.WriteLine(
            //                    @"   
            //            1-Register                           
            //            2-LogIn 
            //           ");
            //                    string choice;
            //                    choice = Console.ReadLine();
            //                    int choiceValue;
            //                    int.TryParse(choice, out choiceValue);
            //                    switch(choiceValue)

            memberSuccess = LoginApp.doMemberLogin();
        } while (!auth && ++count < MAX_LOGINS);

        while (memberSuccess == 0)
        {
            Console.WriteLine("Try Again");
            memberSuccess = LoginApp.doMemberLogin();
        }
        if (memberSuccess == 1)
        {
            List<string> headers = new List<string>();
            string usersessionid = LoginApp.usession;
            string cbrandsessionid = LoginApp.cbsession;
            headers.Add("Authorization:{userSession= " + usersessionid + ",cobSession=" + cbrandsessionid + "}");
            readValue();
        }



    }
    public static void readValue()
    {
        Console.WriteLine("Select the apps to run:");
        Console.WriteLine(
        @"   
            1-Site Search                            
            2-Add Site Account(Non MFA) 
            3-Add Site Account(MFA) 
            4-Add Site Account(Non MFA)  ---NEW
            5-Add Site Account(MFA)   ---NEW
            6-Accounts
            7-Holdings
            8-Transactions
            9-HoldingSummary
            10-HoldingWithAsset
            11-InvestmentOptions
            0-Exit");
        string key;
        key = Console.ReadLine();
        int keyValue;
        int.TryParse(key, out keyValue);
        ProcessInput(keyValue);
    }
    public static void ProcessInput(int keyValue)
    {
        if (keyValue > 12)
        {
            Console.WriteLine("Enter One Valid Number");
            readValue();
        }
        else
        {
            switch (keyValue)
            {
                case 1:
                    Console.WriteLine("Enter the name of the  Provider you want to search : ");
                    String searchString = Console.ReadLine();
                    //String searchString = "Dag";
                    ProviderApp.searchProvider(searchString);
                    LoginApp.readValue();
                    break;
                case 2:
                    //AddSiteAccount.addSiteAccount();
                    AddProviderAccount.AddAccount(1);
                    break;
                case 3:
                    AddProviderAccount.AddAccount(2);
                    break;
                case 4:
                    AddProviderAccountNew.AddAccount(1);
                    break;
                case 5:
                    AddProviderAccountNew.AddAccount(2);
                    break;
                case 6:
                    AccountApp.accountApp(1);
                    break;
                case 7:
                    HoldingApp.holdingApp(1);
                    break;
                case 8:
                    TransactionApp.transactionApp();
                    break;
                case 9:
                    AssetSummaryApp.assetSummaryApp();
                    break;
                case 10:
                    HoldingApp.holdingApp(2);
                    break;
                case 11:
                    AccountApp.accountApp(2);
                    break;
                case 0:


                    Environment.Exit(0);
                    break;

            }
        }
    }
}
}