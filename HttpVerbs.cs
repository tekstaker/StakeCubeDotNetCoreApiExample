using Newtonsoft.Json;
using RestSharp;
using Serilog;
using System;

namespace StakeCubeDotNetCoreApiExample
{
    public static class HttpVerbs
    {


        public static dynamic GETRequest(string path, string apiUrl, string privateKey, string publicKey, string parameters = "")
        {
            Log.Information("START GET Request");
            Log.Information("path: {0}", path);
            Log.Information("parameters: {0}", parameters);

            dynamic content = null;

            try
            {
                string body = String.Concat("nonce=", Cryptography.GetNonce());
                if (!String.IsNullOrEmpty(parameters))
                {
                    body = String.Concat(body, "&", parameters);
                }
                Log.Information("body: {0}", body);

                var signature = Cryptography.SignatureGenerator(body, privateKey);

                var url = String.Concat(apiUrl, path, "?", body, "&signature=", signature);
                Log.Information("url: {0}", url);

                RestClient client = new RestClient(url);
                RestRequest request = new RestRequest(Method.GET);
                request.AddHeader("User-Agent", "Smith");
                request.AddHeader("X-API-KEY", publicKey);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

                var response = client.Execute(request);

                Log.Information(response.Content);

                content = JsonConvert.DeserializeObject(response.Content);
            }
            catch (Exception err)
            {
                Log.Information(err.ToString());
            }

            Log.Information("DONE GET Request");
            return content;
        }

        public static dynamic POSTRequest(string path, string apiUrl, string privateKey, string publicKey, string parameters = "")
        {
            Log.Information("START POST Request");
            Log.Information("path: {0}", path);
            Log.Information("parameters: {0}", parameters);

            dynamic content = null;

            try
            {
                string body = String.Concat("nonce=", Cryptography.GetNonce());
                if (!String.IsNullOrEmpty(parameters))
                {
                    body = String.Concat(body, "&", parameters);
                }
                Log.Information("body: {0}", body);

                var signature = Cryptography.SignatureGenerator(body, privateKey);

                var url = String.Concat(apiUrl, path);
                Log.Information("url: {0}", url);

                RestClient client = new RestClient(url);
                RestRequest request = new RestRequest(Method.POST);
                request.AddHeader("User-Agent", "Smith");
                request.AddHeader("X-API-KEY", publicKey);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

                request.AddParameter("signature", signature);

                var splitParams = body.Split('&');
                foreach (string p in splitParams)
                {
                    var items = p.Split('=');
                    request.AddParameter(items[0], items[1]);
                }



                var response = client.Execute(request);

                Log.Information(response.Content);

                content = JsonConvert.DeserializeObject(response.Content);

            }
            catch (Exception err)
            {
                Log.Information(err.ToString());
            }

            Log.Information("END POST Request");
            return content;
        }

    }
}
