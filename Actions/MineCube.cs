using Serilog;

namespace StakeCubeDotNetCoreApiExample.Actions
{
    class MineCube
    {
        public static dynamic GetMineCubeInfo(string apiUrl, string privateKey, string publicKey)
        {
            Log.Information("START GetMineCubeInfo");

            var url = "/minecube/info";

            var res = HttpVerbs.GETRequest(url, apiUrl, privateKey, publicKey);
            Log.Information("DONE GetMineCubeInfo");

            return res;
        }

        public static dynamic BuyWorkers(string apiUrl, string privateKey, string publicKey)
        {
            var url = "/minecube/buyWorker";
            var parameters = "method=SCC&amount=10";
            var res = HttpVerbs.POSTRequest(url, apiUrl, privateKey, publicKey, parameters);

            return res;
        }
    }
}
