using System;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Maoui
{
    public static class StampHelper
    {
        public static DateTime GetBuildDate(Assembly assembly)
        {
            var attribute = assembly.GetCustomAttributes<AssemblyMetadataAttribute>().FirstOrDefault(x => x.Key == "BuildDate");
            if (attribute != null)
                if (DateTime.TryParseExact(attribute.Value, "yyyyMMddHHmmss", CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out var result))
                    return result;

            return default(DateTime);
        }

        public static string GetCommitHash(Assembly assembly)
        {
            var attribute = assembly.GetCustomAttributes<AssemblyMetadataAttribute>().FirstOrDefault(x => x.Key == "CommitHash");
            if (attribute != null)
                return attribute.Value;

            return string.Empty;
        }
    }
}
