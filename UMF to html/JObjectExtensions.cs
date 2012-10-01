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
                    throw new RequiredFieldNotFoundException(field);
            }
        }
    }

    public class RequiredFieldNotFoundException : Exception
    {
        public RequiredFieldNotFoundException(string field)
            : base(field)
        {
        }
    }
}