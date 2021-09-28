using System;
using System.ComponentModel.DataAnnotations;

namespace GraphQLSample.Shared.Common
{
    public static class ObjectExtensions
    {
        public static void ThrowExceptionIfNull(this object obj, string name)
        {
            if (obj == null)
                throw new ArgumentNullException(name);
        }

        public static void ThrowExceptionIfNullOrEmpty(this string str, string name)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException(name);
        }

        public static bool IsEmailAddress(this string inputText)
        {
            return !string.IsNullOrWhiteSpace(inputText) && new EmailAddressAttribute().IsValid(inputText);
        }

        public static bool HasValue(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        public static bool IsNotEmpty(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }


    }
}