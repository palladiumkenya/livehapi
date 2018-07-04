using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LiveHAPI.Shared.Custom
{
    public static class TypeExtentions
    {
        public static bool IsNullOrEmpty(this DateTime? guid)
        {
            if (null == guid)
                return true;

            if (guid.HasValue && guid.Value==DateTime.MinValue)
                return true;

            if (!guid.HasValue)
                return true;

            return false;
        }

        public static bool IsNullOrEmpty(this DateTime guid)
        {
            if (guid == DateTime.MinValue)
                return true;

            return false;
        }

        /// <summary>
        /// Determines if a nullable Guid (Guid?) is null or Guid.Empty
        /// </summary>
        public static bool IsNullOrEmpty(this Guid? guid)
        {
            if (null == guid)
                return true;

            if (guid.HasValue && guid.Value == Guid.Empty)
                return true;

            if (!guid.HasValue)
                return true;

            return false;
        }

        /// <summary>
        /// Determines if Guid is Guid.Empty
        /// </summary>
        public static bool IsNullOrEmpty(this Guid guid)
        {
            return guid == Guid.Empty;
        }

        public static bool IsSameAs(this object s, object other)
        {
            return s.ToString().ToLower().Trim() == other.ToString().ToLower().Trim();
        }

        public static string Sanitize(this string s)
        {
            return null == s ? string.Empty : s.Replace(@"'", @"''");
        }

        public static Object GetPropValue(this Object obj, String name)
        {
            foreach (String part in name.Split('.'))
            {
                if (obj == null)
                {
                    return null;
                }

                Type type = obj.GetType();
                PropertyInfo info = type.GetProperty(part);
                if (info == null)
                {
                    return null;
                }

                obj = info.GetValue(obj, null);
            }

            return obj;
        }

        public static T GetPropValue<T>(this Object obj, String name)
        {
            Object retval = GetPropValue(obj, name);
            if (retval == null)
            {
                return default(T);
            }

            // throws InvalidCastException if types are incompatible
            return (T) retval;
        }


        public static Task<HttpResponseMessage> PostAsJsonAsync<T>(
            this HttpClient httpClient, string url, T data)
        {
            var dataAsString = JsonConvert.SerializeObject(data);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return httpClient.PostAsync(url, content);
        }

        public static async Task<T> ReadAsJsonAsync<T>(this HttpContent content)
        {
            var dataAsString = await content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(dataAsString);
        }
        public static string ToIqDate(this DateTime? dateTime, string defaultDate = "")
        {
            try
            {
                return dateTime.IsNullOrEmpty() ? defaultDate :  dateTime.Value.ToIqDate();
            }
            catch
            {
                return string.Empty;
            }
        }
        public static string ToIqDateOnly(this DateTime? dateTime,string defaultDate="")
        {
            try
            {
                return dateTime.IsNullOrEmpty() ? defaultDate :  dateTime.Value.ToIqDateOnly();
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string ToIqDate(this DateTime dateTime)
        {
            try
            {
                return dateTime.IsNullOrEmpty() ? "" : dateTime.Date.ToString("yyyyMMddHHmmss");
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string ToIqDateOnly(this DateTime dateTime)
        {
            try
            {
                return dateTime.IsNullOrEmpty() ? "" : dateTime.Date.ToString("yyyyMMdd");

            }
            catch 
            {
                return string.Empty;
            }
        }
    }
}