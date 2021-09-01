using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Homo.AuthApi;

namespace Homo.CmsApi
{
    public class CmsAuthorizeFactory : ActionFilterAttribute, IFilterFactory
    {
        public bool IsReusable => true;
        public CMS_ROLE[] _cmsRoles;
        public AUTH_TYPE _authType = AUTH_TYPE.COMMON;
        public CmsAuthorizeFactory(AUTH_TYPE type = AUTH_TYPE.COMMON, CMS_ROLE[] roles = null)
        {
            this._authType = type;
            this._cmsRoles = roles == null ? new CMS_ROLE[] { CMS_ROLE.NO } : roles;
        }

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            IOptions<AppSettings> config = serviceProvider.GetService<IOptions<AppSettings>>();
            var secrets = (Secrets)config.Value.Secrets;
            return AuthorizeFactory.BuildAuthorizeAttribute(
                _cmsRoles.Select(x => x.ToString()).ToArray()
                , config.Value.Common.AuthByCookie
                , _authType
                , secrets.JwtKey
                , secrets.SignUpJwtKey
                , secrets.AnonymousJwtKey); ;
        }
    }
}