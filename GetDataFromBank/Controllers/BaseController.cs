using GetDataFromBank.Models;
using GetDataFromBank.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GetDataFromBank.Controllers
{
    public class BaseController : Controller
    {

        public static string usession { get; set; }
        public static string cbsession { get; set; }
        private static readonly string fqcn = typeof(BaseController).FullName;
        public static IDictionary<string, string> loginTokens = new Dictionary<string, string>();
        public static string localURLVer1 = ConfigurationManager.AppSettings["url"];//"http://localhost:8080/ysl/yodlee/";
        // GET: Base
        public ActionResult Index()
        {
            CredencialesViewModel cred = new CredencialesViewModel();
            int MAX_LOGINS = 1, memberSuccess, cobrandSuccess;
            bool auth = false;
            int count = 0;
            do
            {

                cobrandSuccess = LoginApp.doCoBrandLogin();
                while (cobrandSuccess == 0)
                {
                   
                    this.Index();

                }
                //                    
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
               
                memberSuccess = LoginApp.doMemberLogin();
            }
            if (memberSuccess == 1)
            {
                List<string> headers = new List<string>();
                string usersessionid = LoginApp.usession;
                string cbrandsessionid = LoginApp.cbsession;
                headers.Add("Authorization:{userSession= " + usersessionid + ",cobSession=" + cbrandsessionid + "}");
                cred.CoBrandauth = usersessionid;
                cred.Memberauth = cbrandsessionid;
            }


               

           //cred.auth= doLogin("sbCobd711dae4ac2ebf69822f2e9f34d71738ba", "ff17cb34-c563-4b33-8042-251683b3a06b", "sbMemd711dae4ac2ebf69822f2e9f34d71738ba3", "sbMemd711dae4ac2ebf69822f2e9f34d71738ba3#123");

            return View(cred);
        }







        public static string doLogin(string coBrandUserName, string coBrandPassword, string userName, string userPassword)
        {

            //doCoBrandLogin(coBrandUserName, coBrandPassword);

            //cobrandSuccess = LoginApp.doCoBrandLogin();
            string res=  doMemberLogin(coBrandUserName, coBrandPassword,userName,userPassword);

            return res;
        }

        public static string doMemberLogin(string coBrandUserNa, string coBrandPass, string userNa, string userPass)
        {
            //Console.WriteLine("Enter your userName: ");
            string userName = ConfigurationManager.AppSettings["cobUserNameQA"]; //Console.ReadLine(); //"novlessthan500";//"sbMemtestmar212";//
                                     // Console.WriteLine("Enter " + userName + "'s password");
            string userPassword =  ConfigurationManager.AppSettings["cobPasswordQA"];  //Console.ReadLine();//"TEST@123";// "sbMemtestmar212#123"; //
            //string mn = "doMemberLogin(loginName=" + userName + ", password = " + userPassword + ", coBrandSessionCredential =" + loginTokens["cobSession"] + " )";
            //string requestBody = "coBrandSessionCredential=" + loginTokens["cobSession"] + "&loginName=" + userName + "&password=" + userPassword;
            string requestBody = "{\"user\": {\"loginName\":\"" + userName + "\",\"password\":\"" + userPassword + "\"}}";
            //loginTokens.Add("cobSession", cbsession);

            string userLoginURL = localURLVer1 + "cobrand/login";
            //string userLoginURL =  localURLVer1 + coBrandUserNa + " / v1/cobrand/login";
            //string cobses = loginTokens["cobSession"];//cobrand session id
            string cobses = cbsession;//  loginTokens["cobSession"];//cobrand session id
            List<string> headers = new List<string>();
            headers.Add("Authorization:{cobSession= " + cobses + "}");//passing cobrand session id.
            string jsonResponse = HTTP.doPostUser(userLoginURL, headers, requestBody, null);
            if (jsonResponse == null)
            {
                Console.WriteLine("Wrong username/password");
                return "0";

            }
            else
            {
                UserContext member = (UserContext)GSONParser.handleJson(jsonResponse, typeof(UserContext));
                Console.WriteLine("--- User Session Id ---");
                Console.WriteLine(member.user.session.userSession);
                Console.WriteLine("-----------------------");
                loginTokens["userSession"] = member.user.session.userSession;
                usession = loginTokens["userSession"];
                return member.user.session.userSession;
            }


        }

    }
}