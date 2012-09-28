using System;
using Newtonsoft.Json.Linq;

namespace UMF
{
    public static class JObjectExtensions
    {
        public static void ValidateRequiredFields(this JObject jObject, params string[] requiredFields)
        {
            foreach (var field in requiredFields)
            {
                if (jObject[field] == null)
                    throw new Exception(String.Format("'{0}' required field is missing", field));
            }
        }
    }
}