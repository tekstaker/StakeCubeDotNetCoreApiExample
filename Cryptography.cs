using Serilog;
using System;
using System.Security.Cryptography;
using System.Text;

namespace StakeCubeDotNetCoreApiExample
{
    public static class Cryptography
    {
        #region Fields
        private static readonly Encoding encoding = Encoding.UTF8;
        #endregion Fields

        public static long GetNonce()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }




        //from: https://stackoverflow.com/questions/12804231/c-sharp-equivalent-to-hash-hmac-in-php
        public static string SignatureGenerator(string data, string secret)
        {
            string res = null;

            try
            {
                var keyByte = encoding.GetBytes(secret);
                using (var hmacsha256 = new HMACSHA256(keyByte))
                {
                    hmacsha256.ComputeHash(encoding.GetBytes(data));
                    res = ByteToString(hmacsha256.Hash).ToLower();

                    Log.Information("Signature Result: {0}", res);
                }
            }
            catch (Exception ex)
            {
                Log.Information(ex.ToString());
            }

            return res;
        }

        static string ByteToString(byte[] buff)
        {
            string sbinary = "";
            for (int i = 0; i < buff.Length; i++)
                sbinary += buff[i].ToString("X2"); /* hex format */
            return sbinary;
        }
    }
}
