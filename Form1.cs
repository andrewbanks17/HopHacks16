using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web.Script.Serialization;
using System.Net;
using System.IO;
//possibly fit transaction rate, otherwise im done
//main checking id: 580b8d3b360f81f104544ced
//budget id: 580c2363360f81f104544f26
//the store acts as the server and the shop... spaghetti never tasted so good
//hide sell buttons for now o boy o geez
namespace HopHacksShopSimulation
{
    //transfer from main to budget
    //http://api.reimaginebanking.com/accounts/580b8d3b360f81f104544ced/transfers?key=8718dbc034540cb9f0b1b76d42452753
    /*{
  "medium": "balance",
  "payee_id": "580c2363360f81f104544f26",
  "amount": 1000,
  "transaction_date": "2016-10-23",
  "description": "string"
    }
    */

    //bill request for :580c3da7360f81f104544f5c
    //http://api.reimaginebanking.com/bills/580c3da7360f81f104544f5c?key=8718dbc034540cb9f0b1b76d42452753
    

    public partial class Form1 : Form
    {
        /**MAIN ACCOUNT**/
        //protected string purchaseUrl = "http://api.reimaginebanking.com/accounts/580b8d3b360f81f104544ced/purchases?key=8718dbc034540cb9f0b1b76d42452753";//purchases from a specific merchant
        //protected string depositUrl = "http://api.reimaginebanking.com/accounts/580b8d3b360f81f104544ced/deposits?key=8718dbc034540cb9f0b1b76d42452753";//deposits to the checking account

        /**BUDGET ACCOUNT**/
        protected string purchaseUrl = "http://api.reimaginebanking.com/accounts/580c2363360f81f104544f26/purchases?key=8718dbc034540cb9f0b1b76d42452753";//purchases from a specific merchant
        protected string depositUrl = "http://api.reimaginebanking.com/accounts/580c2363360f81f104544f26/deposits?key=8718dbc034540cb9f0b1b76d42452753";//deposits to the checking account

        double currentBudget = 0;
        bool isOver = false;
        bool firstOverflow = true;
        public Form1()
        {
            string result = "";
            //gets current budget of budget account

             using (var client = new WebClient())
             {
                 client.Headers[HttpRequestHeader.ContentType] = "application/json";
                 result = client.DownloadString("http://api.reimaginebanking.com/accounts/580c2363360f81f104544f26?key=8718dbc034540cb9f0b1b76d42452753");//gets budget accoutn
                 Account deserializedAccount = new JavaScriptSerializer().Deserialize<Account>(result); //deserializes the get request
                 Console.WriteLine("Budget Account: " + deserializedAccount);
                 currentBudget = deserializedAccount.balance;
                 Console.WriteLine("CURRENT BUDGET IS: " + currentBudget);
             }
           if(currentBudget <= 0)
            {
                transfer(1000);
            }
           //overBudget(20);//
           //transfer(20);
                InitializeComponent();
            this.FormClosing += Form1_Closing;
        }

        //bed buy 3000
        private void button1_Click(object sender, EventArgs e)
        {
            string result = "";
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";

                var obj = new Purchase
                {
                    merchant_id = "580b95b1360f81f104544d1c",
                    medium = "balance",
                    purchase_date = "2016-10-23",
                    amount = 3000,
                    description = "cSharp bed"

                };
                currentBudget -= obj.amount;

                if(currentBudget < 0)
                {
                    overBudget(obj.amount);
                }

                var json = new JavaScriptSerializer().Serialize(obj);

                result = client.UploadString(purchaseUrl, "POST", json);
            }
            Console.WriteLine(result);
        }

        //char buy 100
        private void button2_Click(object sender, EventArgs e)
        {
            string result = "";
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";

                var obj = new Purchase
                {
                    merchant_id = "580b95b1360f81f104544d1c",
                    medium = "balance",
                    purchase_date = "2016-10-23",
                    amount = 100,
                    description = "cSharp Chair"

                };
                currentBudget -= obj.amount;

                if (currentBudget < 0)
                {
                    overBudget(obj.amount);
                }
                var json = new JavaScriptSerializer().Serialize(obj);

                result = client.UploadString(purchaseUrl, "POST", json);
            }
            Console.WriteLine(result);
        }
        //potato buy 5
        private void button3_Click(object sender, EventArgs e)
        {
            string result = "";
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";

                var obj = new Purchase
                {
                    merchant_id = "580b95b1360f81f104544d1c",
                    medium = "balance",
                    purchase_date = "2016-10-23",
                    amount = 5,
                    description = "cSharp Potato"

                };
                currentBudget -= obj.amount;

                if (currentBudget < 0)
                {
                    overBudget(obj.amount);
                }
                var json = new JavaScriptSerializer().Serialize(obj);

                result = client.UploadString(purchaseUrl, "POST", json);
            }
            Console.WriteLine(result);
        }

        //snow plow sell 50
        private void button6_Click(object sender, EventArgs e)
        {
            string result = "";
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";

                var obj = new Deposit
                {
                    medium = "balance",
                    transaction_date = "2016-10-23",
                    amount = 50,
                    description = "plowed and got money"
                };
                var json = new JavaScriptSerializer().Serialize(obj);

                result = client.UploadString(depositUrl, "POST", json);
            }
            Console.WriteLine(result);
        }

        //rake leaves sell 24
        private void button5_Click(object sender, EventArgs e)
        {
            string result = "";
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";

                var obj = new Deposit
                {
                    medium = "balance",
                    transaction_date = "2016-10-23",
                    amount = 24,
                    description = "raked and made bank"
                };
                var json = new JavaScriptSerializer().Serialize(obj);

                result = client.UploadString(depositUrl, "POST", json);
            }
            Console.WriteLine(result);
        }

        //walked a dog sell 70
        private void button4_Click(object sender, EventArgs e)
        {
            string result = "";
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";

                var obj = new Deposit
                {
                    medium = "balance",
                    transaction_date = "2016-10-23",
                    amount = 70,
                    description = "walked and talked"
                };
                var json = new JavaScriptSerializer().Serialize(obj);

                result = client.UploadString(depositUrl, "POST", json);
            }
            Console.WriteLine(result);
        }

        //when ever over budget. make sure stuff is reset though at close. passed in amount is what is taken from savings/ checkings?
        private void overBudget(double inAmount)
        {
            //changes nickname of bill from under to over..
            //the trick is im using one of the bills fields as a super global variable.
            //sets budget to 0 and deducts excess from savings
            var result = "";
            double partial = 0;
            //first purchase spends all we have left of current budget
            if (firstOverflow)
            {
                partial = currentBudget +inAmount;
                Console.WriteLine(partial);
                using (var client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";

                    var obj = new Purchase
                    {
                        merchant_id = "580b95b1360f81f104544d1c",
                        medium = "balance",
                        purchase_date = "2016-10-23",
                        amount = partial,
                        description = "cSharp bed part 1"

                    };
                    var json = new JavaScriptSerializer().Serialize(obj);

                    result = client.UploadString(purchaseUrl, "POST", json);
                }
                firstOverflow = false;
            }
            //2nd purchase spends all we have left of current budget
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";

                var obj = new Purchase
                {
                    merchant_id = "580b95b1360f81f104544d1c",
                    medium = "balance",
                    purchase_date = "2016-10-23",
                    amount = (inAmount - partial),
                    description = "cSharp bed part 2"

                };
                var json = new JavaScriptSerializer().Serialize(obj);

                result = client.UploadString("http://api.reimaginebanking.com/accounts/580b8d3b360f81f104544ced/purchases?key=8718dbc034540cb9f0b1b76d42452753", "POST", json);//this is the checking acc. pay from it when no funds
            }
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";

                result = client.UploadString("http://api.reimaginebanking.com/bills/580c3da7360f81f104544f5c?key=8718dbc034540cb9f0b1b76d42452753", "PUT", "{ \"nickname\": \"OVER\"}");//modifies bill nickname setting so watch vibrates
                Console.WriteLine("Budget Account: " + result+"?");                
            }
            //transfer(inAmount);//basically allows person to still buy, but it comes out of their pocket nope
        }
       
        private void transfer(double inAmount)
        {

            // I HONESTLY HAVE NO IDEA WHY THIS DIDNT WORK?????????????????????
          /*  using (var client = new WebClient())
            {
                var obj = new Transferr
                {
                medium = "balance",
                payee_id = "580c2363360f81f104544f26",
                amount = 20,
                transaction_date = "2016-10-22",
                description = "overflow"

                };
            var json = new JavaScriptSerializer().Serialize(obj);
            Console.WriteLine(json);
            string result = client.UploadString("http://api.reimaginebanking.com/accounts/580b8d3b360f81f104544ced/transfers?key=8718dbc034540cb9f0b1b76d42452753", "POST", ("{ \"medium\":\"balance\",\"payee_id\":\"580c2363360f81f104544f26\",\"amount\":20,\"transaction_date\":\"2016-10-22\",\"description\":\"overflow\"}"));
            }

    */
            //other method to transfer... just go with it
            var request = (HttpWebRequest)WebRequest.Create("http://api.reimaginebanking.com/accounts/580b8d3b360f81f104544ced/transfers?key=8718dbc034540cb9f0b1b76d42452753");
            request.ContentType = "application/json";
            request.Accept = "application/json";
            request.Method = "POST";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                var obj = new Transfer
                {
                    medium = "balance",
                    payee_id = "580c2363360f81f104544f26",
                    amount = inAmount,
                    transaction_date = "2016-10-23",
                    description = ("overflow of " + inAmount+"")

                };
                var json = new JavaScriptSerializer().Serialize(obj);

                streamWriter.Write(json);
            }
            
            
            var response = (HttpWebResponse)request.GetResponse();
            
            
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                Console.WriteLine("result of transfer: " + result);
            }
        }
        private void Form1_Closing(object sender, FormClosingEventArgs e)//resets the budget so it isnt overflwoign anymore. basically for demo purposes
        {
            //changes nickname of bill from under to over..
            //the trick is im using one of the bills fields as a super global variable.
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";

                String result = client.UploadString("http://api.reimaginebanking.com/bills/580c3da7360f81f104544f5c?key=8718dbc034540cb9f0b1b76d42452753", "PUT", "{ \"nickname\": \"UNDER\"}");//modifies bill nickname setting
                Console.WriteLine("Budget Account: " + result + "?");
                isOver = true;
            }
        }
    }

    public class Purchase
    {
        public string merchant_id;
        public string medium;
        public string purchase_date;
        public double amount;
        public string description;
    }

    public class Deposit
    {
        public string medium;
        public string transaction_date;
        public double amount;
        public string description;

    }
    //transfers budget from top to bot every day... or at recreatino of progrM
    public class Transfer
    {
        public string medium;
        public string payee_id;
        public double amount;
        public string transaction_date;
        public string description;

    }

    public class Bill
    {
        public string status;
        public string payee;
        public string nickname;
        public string payment_date;
        public int recurring_date;
        public double amount;       
    }
    //from get account/{ids}
    public class Account
    {
        public string _id;
        public string type;
        public int rewards;
        public int balance;
        public string nickname;
        public string account_number;
        public string customer_id;
    }
}
