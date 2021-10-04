using Serilog;

namespace StakeCubeDotNetCoreApiExample.Actions
{
    class User
    {
        public static dynamic GetAccountInfo(string apiUrl, string privateKey, string publicKey)
        {
            Log.Information("START GetAccountInfo");

            var url = "/user/account";
            var res = HttpVerbs.GETRequest(url, apiUrl, privateKey, publicKey);

            Log.Information("DONE GetAccountInfo");

            return res;
        }
    }
}
