using System;
using System.Text;

namespace NettyBase.Utils
{
    static class Decode
    {
        public static string DecodeFrom64(string encodedData)
        {
            byte[] encodedDataAsBytes = Convert.FromBase64String(encodedData);
            return Encoding.ASCII.GetString(encodedDataAsBytes);
        }
    }
}
