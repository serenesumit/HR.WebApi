using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.WebApi.Common
{
    public static class Constants
    {

        public static class Azure
        {
            public static class Containers
            {
                public const string PageAssets = "documents";
            }

            public static class BlobPaths
            {
                // documents/{documentid}
                public const string Docs = "documents/{0}";
            }

            public static class ShareAccessPolicies
            {
                public const string TenMinutesDownloadPolicy = "TenMinutesDownloadPolicy";
            }
        }

    }
}
