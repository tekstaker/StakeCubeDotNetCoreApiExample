using System;
using System.Collections.Generic;
using System.Text;

namespace StakeCubeDotNetCoreApiExample.Actions
{
    class Exchange
    {
        public static dynamic ArbitrageInfo(string ticker, string apiUrl, string privateKey, string publicKey)
        {
            var url = "/exchange/spot/arbitrageInfo";
            var parameters = $"ticker={ticker}";
            var res = HttpVerbs.GETRequest(url, apiUrl, privateKey, publicKey, parameters);

            return res;
        }

        public static dynamic MyTrades(string market, int limit, string apiUrl, string privateKey, string publicKey)
        {
            var url = "/exchange/spot/myTrades";
            var parameters = $"market={market}&limit={limit}";
            var res = HttpVerbs.GETRequest(url, apiUrl, privateKey, publicKey, parameters);

            return res;
        }
    }
}
