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
                public const string PageAssets = "resumes";

                public const string PageContactAssets = "contacts";
            }

            public static class BlobPaths
            {
                // resumes/{resumesId}
                public const string EmployeeResumes = "resumes/{0}";

                public const string ContactDocs = "contacts/{0}";
            }

            public static class ShareAccessPolicies
            {
                public const string TenMinutesDownloadPolicy = "TenMinutesDownloadPolicy";
            }
        }

    }
}
