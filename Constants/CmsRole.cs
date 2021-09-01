using System.ComponentModel;
using Homo.AuthApi;

namespace Homo.CmsApi
{
    public enum CMS_ROLE
    {
        NO = ROLE.NO,
        ADMIN = ROLE.ADMIN,

        [Description("Article Manage")]
        ARTICLE

    }
}