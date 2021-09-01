using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Homo.Core.Constants;
using Homo.AuthApi;


namespace Homo.CmsApi
{
    [Route("v1/articles")]
    [CmsAuthorizeFactory(AUTH_TYPE.COMMON, new CMS_ROLE[] { CMS_ROLE.ARTICLE })]
    public class ArticleController : ControllerBase
    {
        private readonly CmsDbContext _dbContext;
        public ArticleController(CmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<dynamic> getList([FromQuery] int limit, [FromQuery] int page, [FromQuery] string name)
        {
            List<Article> records = ArticleDataservice.GetList(_dbContext, page, limit, name);
            return new
            {
                articles = records,
                rowNums = ArticleDataservice.GetRowNum(_dbContext, name)
            };
        }

        [HttpGet]
        [Route("all")]
        public ActionResult<dynamic> getAll([FromQuery] string name)
        {
            return ArticleDataservice.GetAll(_dbContext, name);
        }

        [HttpPost]
        public ActionResult<dynamic> create([FromBody] DTOs.Article dto, dynamic extraPayload)
        {
            long createdBy = (long)extraPayload.userId.Value;
            Article rewRecord = ArticleDataservice.Create(_dbContext, createdBy, dto);
            return rewRecord;
        }

        [HttpDelete]
        public ActionResult<dynamic> batchDelete([FromBody] List<long?> ids, dynamic extraPayload)
        {
            long editedBy = (long)extraPayload.userId.Value;
            ArticleDataservice.BatchDelete(_dbContext, editedBy, ids);
            return new { status = CUSTOM_RESPONSE.OK };
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<dynamic> get([FromRoute] int id)
        {
            Article record = ArticleDataservice.GetOne(_dbContext, id);
            if (record == null)
            {
                throw new CustomException(ERROR_CODE.DATA_NOT_FOUND, System.Net.HttpStatusCode.NotFound);
            }
            return record;
        }

        [HttpPatch]
        [Route("{id}")]
        public ActionResult<dynamic> update([FromRoute] int id, [FromBody] DTOs.Article dto, dynamic extraPayload)
        {
            long editedBy = (long)extraPayload.userId.Value;
            ArticleDataservice.Update(_dbContext, id, editedBy, dto);
            return new { status = CUSTOM_RESPONSE.OK };
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult<dynamic> delete([FromRoute] long id, dynamic extraPayload)
        {
            long editedBy = (long)extraPayload.userId.Value;
            ArticleDataservice.Delete(_dbContext, id, editedBy);
            return new { status = CUSTOM_RESPONSE.OK };
        }
    }
}
