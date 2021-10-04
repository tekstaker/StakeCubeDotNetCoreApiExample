using Microsoft.Extensions.Configuration;
using Serilog;
using System;

namespace StakeCubeDotNetCoreApiExample
{
    class Program
    {


        #region Properties
        private static IConfiguration _configuration;
        private static IConfiguration Configuration
        {
            get
            {
                return _configuration;
            }
            set
            {
                _configuration = value;
            }

        }


        private static string _apiUrl;
        private static string ApiUrl
        {
            get
            {
                if (String.IsNullOrEmpty(_apiUrl))
                {
                    _apiUrl = _configuration["apiUrl"];
                }

                return _apiUrl;
            }
        }

        private static string _publicKey;
        private static string PublicKey
        {
            get 
            {
                if(String.IsNullOrEmpty(_publicKey))
                {
                    _publicKey = _configuration["publicKey"];
                }

                return _publicKey;
            }
        }

        private static string _privateKey;
        private static string PrivateKey
        {
            get 
            {
                if(String.IsNullOrEmpty(_privateKey))
                {
                    _privateKey = _configuration["privateKey"];
                }

                return _privateKey;
            }
        }


        #endregion Properties

        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration().WriteTo.Console().WriteTo.RollingFile("log.txt").CreateLogger();

            Log.Information("#########################");
            Log.Information("Starting Stake Cube API Example");
            Log.Information("#########################");

            Configuration = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json", true, true)
           .Build();

            Log.Information("Settings:");
            Log.Information($"apiUrl:  {ApiUrl}");
            Log.Information($"privateKey:  {PrivateKey}");
            Log.Information($"publicKey:  {PublicKey}");

            TestApi(ApiUrl, PrivateKey, PublicKey);

            Console.WriteLine("Press any key to close this window.");
            Console.ReadKey();
        }

        static void TestApi(string apiUrl, string privateKey, string publicKey)
        {
            //## System tests
            var rl = Actions.System.GetRateLimits(apiUrl, privateKey, publicKey);

            //## Exchange tests
            var ai = Actions.Exchange.ArbitrageInfo("SCC", ApiUrl, PrivateKey, PublicKey);

            var mt = Actions.Exchange.MyTrades("SCC_BTC", 100, ApiUrl, PrivateKey, PublicKey);

            //## User tests
            var user = Actions.User.GetAccountInfo(ApiUrl, PrivateKey, PublicKey);

            //## Minecube tests
            var mci = Actions.MineCube.GetMineCubeInfo(ApiUrl, PrivateKey, PublicKey);

            var bal = (decimal)user.SelectToken("$..wallets[?(@.asset == 'SCC')].balance");
            Log.Information("SCC balance is: {0}", bal);
        }
    }
}

