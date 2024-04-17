using System.Collections.Generic;
using System.Text;

namespace BufTools.AspNet.TestFramework
{
    /// <summary>
    /// A set of extension methods for a URI
    /// </summary>
    public static class UriExtensions
    {
        /// <summary>
        /// Adds a query params to a uri
        /// </summary>
        /// <param name="uri">The URI that will is getting params added</param>
        /// <param name="queryParams">The params to add</param>
        /// <returns>A <see cref="string"/>containing the params</returns>
        public static string WithQueryParams(this string uri, IDictionary<string, string?> queryParams)
        {
            var sb = new StringBuilder(uri);
            var firstParam = false;

            if (!uri.Contains("?"))
            {
                sb.Append("?");
                firstParam = true;
            }

            foreach (var param in queryParams)
            {
                if (!firstParam)
                    sb.Append("&");

                sb.Append(param.Key);

                if (param.Value != null)
                {
                    sb.Append("=");
                    sb.Append(param.Value);
                }

                firstParam = false;
            }
            return sb.ToString();
        }
    }
}
