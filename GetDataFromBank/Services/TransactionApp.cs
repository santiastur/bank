using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GetDataFromBank.Services
{
    public class TransactionApp
    {
        private static readonly string fqcn = typeof(TransactionApp).FullName;
        public static Transactions Transactions
        {
            get
            {
                string mn = "getTransactions()";
                string transactionsURL = LoginApp.localURLVer1 + "transactions/";
                List<string> headers = new List<string>();
                string usersessionid = LoginApp.usession;
                string cbrandsessionid = LoginApp.cbsession;
                headers.Add("Authorization:{userSession= " + usersessionid + ",cobSession=" + cbrandsessionid + "}");
                string jsonResponse = HTTP.doGet(transactionsURL, headers);//heades-Authorization headers i.e-member session id and cobrand sesion id.
                Type transtype = typeof(Transactions);
                Transactions transactions = (Transactions)GSONParser.handleJson(jsonResponse, transtype);
                return transactions;
            }
        }
        public static void transactionApp()
        {
            Console.WriteLine("TransactionApp - TEST - START");
            //LoginApp.doLogin("yodlee_10000004", "yodlee123", "sprint13last30days", "TEST@123");
            Transactions transactions = TransactionApp.Transactions;
            Console.WriteLine("-------------------------------");
            Console.WriteLine("TransactionId Amount BaseType");
            Console.WriteLine("-------------------------------");
            Console.WriteLine(transactions.ToString());
            Console.WriteLine("-------------------------------");
            LoginApp.readValue();
            //Console.ReadLine();
        }

        public static void Main(string[] a)
        {
            transactionApp();
        }
    }
}