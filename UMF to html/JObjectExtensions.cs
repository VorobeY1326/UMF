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
        private readonly string field;

        public RequiredFieldNotFoundException(string field)
        {
            this.field = field;
        }

        public override string ToString()
        {
            return field;
        }
    }
}