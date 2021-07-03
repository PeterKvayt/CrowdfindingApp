using System;
using System.Collections.Generic;
using System.Linq;

namespace CrowdfundingApp.Common.Extensions
{
    public static class GuidIEnumerableExtensions
    {
        public static bool AnyNonEmpty(this IEnumerable<Guid> list)
        {
            return list?.Any(x => !x.Equals(Guid.Empty)) ?? false;
        }
    }
}
