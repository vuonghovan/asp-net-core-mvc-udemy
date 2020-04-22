using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Utilities
{
    public static class JsonConverterHelper
    {
        public static T DeserializeObject<T>(string json) where T : class
        {
            try
            {
                if (string.IsNullOrEmpty(json.Trim()))
                    return null;
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static JObject DeserializeObject(string json)
        {
            try
            {
                if (string.IsNullOrEmpty(json.Trim()))
                    throw new Exception("Json Null!");
                return JsonConvert.DeserializeObject<JObject>(json);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static JArray DeserializeArray(string json)
        {
            try
            {
                if (string.IsNullOrEmpty(json.Trim()))
                    throw new Exception("Json Null!");
                return JsonConvert.DeserializeObject<JArray>(json);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<T> DeserializeListObject<T>(string json) where T : class
        {
            try
            {
                if (string.IsNullOrEmpty(json.Trim()))
                    throw new Exception("Json Null!");
                return JsonConvert.DeserializeObject<List<T>>(json);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<T> DeserializeArrayToList<T>(string json) where T : class
        {
            try
            {
                if (string.IsNullOrEmpty(json.Trim()))
                    throw new Exception("Json Null!");
                var temp = JsonConvert.DeserializeObject<JObject>(json);
                JArray array = (JArray)temp["data"];
                return array.ToObject<List<T>>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string SerializeObject<T>(T json) where T : class
        {
            try
            {
                if (json == null)
                    throw new Exception("Json Null!");
                return JsonConvert.SerializeObject(json);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string SerializeObject(Object json)
        {
            try
            {
                if (json == null)
                    throw new Exception("Json Null!");
                return JsonConvert.SerializeObject(json, settings: new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string SerializeListObject<T>(List<T> json) where T : class
        {
            try
            {
                if (json == null)
                    throw new Exception("Json Null!");
                return JsonConvert.SerializeObject(json, settings: new JsonSerializerSettings() { Formatting = Formatting.Indented, NullValueHandling = NullValueHandling.Ignore, ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<T> DeserializeArrayToList<T>(T[] array) where T : class
        {
            try
            {
                if (array == null)
                    return new List<T>();
                return array.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
