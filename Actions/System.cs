using Serilog;

namespace StakeCubeDotNetCoreApiExample.Actions
{
    class System
    {
        public static dynamic GetRateLimits(string apiUrl, string privateKey, string publicKey)
        {
            Log.Information("START GetRateLimits");

            var url = "/system/rateLimits";
            var res = HttpVerbs.GETRequest(url, apiUrl, privateKey, publicKey);

            Log.Information("DONE GetRateLimits");

            return res;
        }
    }
}
