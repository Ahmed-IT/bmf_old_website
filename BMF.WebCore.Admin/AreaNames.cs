using System.Collections.Generic;

namespace BMF.WebCore.Admin
{
    public static class AreaNames
    {
        public const string Security = "Security";

        public const string Location = "Location";

        public const string WebsiteCMS = "WebsiteCMS";

        public const string EMS = "EMS";

        public static List<string> All = new List<string>() { Security, Location, WebsiteCMS, EMS };
    }
}
