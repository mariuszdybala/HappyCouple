using System;
using HappyCoupleMobile.Model;
using Xamarin.Forms;

namespace HappyCoupleMobile.Helpers
{
    public static class StringHelper
    {
        public static string GetBindableName(this string stringToCropp)
        {
            string stringToRemove = "Property";

            int index = stringToCropp.IndexOf(stringToRemove, StringComparison.Ordinal);

            var result = index < 0 ? stringToCropp : stringToCropp.Remove(index, stringToRemove.Length);

            return result;
        }
    }
}