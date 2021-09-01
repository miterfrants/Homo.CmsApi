using System;
using Homo.Api;
using System.Collections.Generic;

namespace Homo.CmsApi
{
    public abstract partial class DTOs
    {
        public partial class Article : DTOs
        {
            public DateTime? PublishAt { get; set; }
            public DateTime? StartAt { get; set; }
            public DateTime? EndAt { get; set; }
            public long? CreatedBy { get; set; }
            public long? EditedBy { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }
            public string Cover { get; set; }
            public List<string> Tag { get; set; }
        }
    }
}
