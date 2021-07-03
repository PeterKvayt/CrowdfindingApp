using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrowdfundingApp.Common.Extensions
{
    public static class StringIEnumerableExtensions
    {
        public static bool AnyNonEmptyOrWhitespace(this IEnumerable<string> list)
        {
            return list?.Any(x => !x.IsNullOrWhiteSpace()) ?? false;
        }
    }
}
