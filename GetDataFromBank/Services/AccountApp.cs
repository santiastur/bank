﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GetDataFromBank.Services
{
    public class AccountApp
    {

        private static readonly string fqcn = typeof(AccountApp).FullName;
        public static Accounts getAccounts()
        {
            string mn = "getAccounts()";
            Console.WriteLine(fqcn + " :: " + mn);
            string accountSummaryURL = LoginApp.localURLVer1 + "accounts/";
            List<string> headers = new List<string>();
            string usersessionid = LoginApp.usession;
            string cbrandsessionid = LoginApp.cbsession;
            headers.Add("Authorization:{userSession= " + usersessionid + ",cobSession=" + cbrandsessionid + "}");
            string jsonResponse = HTTP.doGet(accountSummaryURL, headers);//headers-authorization headers i.e-member external sesionid,cobrand external session id
            Type acc = typeof(Accounts);
            Accounts accounts = (Accounts)GSONParser.handleJson(jsonResponse, acc);
            return accounts;
        }

        public static Accounts getAccountsWithOptions()
        {
            string mn = "getAccounts()";
            Console.WriteLine(fqcn + " :: " + mn);
            string accountSummaryURL = LoginApp.localURLVer1 + "accounts/investmentPlan/investmentOptions/";
            List<string> headers = new List<string>();
            string usersessionid = LoginApp.usession;
            string cbrandsessionid = LoginApp.cbsession;
            headers.Add("Authorization:{userSession= " + usersessionid + ",cobSession=" + cbrandsessionid + "}");
            string jsonResponse = HTTP.doGet(accountSummaryURL, headers);//headers-authorization headers i.e-member external sesionid,cobrand external session id
            Type acc = typeof(Accounts);
            Accounts accounts = (Accounts)GSONParser.handleJson(jsonResponse, acc);
            //string type = "";
            //for (int i = 0; i < accounts.account.Count; i++)
            //{
            //    type = accounts.account[i].AccountType;
            //    if (type == "IRA")
            //    {
            //         string holdingURL = LoginApp.localURLVer1 + "holdings/v1/";
            //         jsonResponse = HTTP.doGet(holdingURL, headers);//headers-authorization headers i.e-member external sesionid,cobrand external session id
            //        Type holding = typeof(Holdings);
            //        Holdings holdings = (Holdings)GSONParser.handleJson(jsonResponse, holding);
            //        Console.WriteLine(holdings.ToString());

            //    }
            //}
            return accounts;
        }

        public static void accountApp(int id)
        {
            if (id == 1)
            {
                Console.WriteLine("AccountApp - TEST - START");
                //LoginApp.doLogin("yodlee_10000004", "yodlee123", "ysluser2", "TEST@123");
                Accounts accounts = AccountApp.getAccounts();
                Console.WriteLine("------------------------");
                Console.WriteLine("Account Id  Account Name");
                Console.WriteLine("------------------------");
                Console.WriteLine(accounts.ToString());
                Console.WriteLine("------------------------");
            }
            else
            {
                Console.WriteLine("AccountApp - TEST - START");
                //LoginApp.doLogin("yodlee_10000004", "yodlee123", "ysluser2", "TEST@123");
                Accounts accounts = AccountApp.getAccountsWithOptions();
                Console.WriteLine("------------------------");
                Console.WriteLine("AccountId  PlanName ProviderName CusipNumber Description HoldingType Price");
                Console.WriteLine("------------------------");
                Console.WriteLine(accounts.ToString());
                Console.WriteLine("------------------------");
            }
            LoginApp.readValue();

        }

        public static void Main(string[] a)
        {
            accountApp(1);
        }
    }
}