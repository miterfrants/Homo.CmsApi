using System;
using Homo.Api;
using System.Collections.Generic;

namespace Homo.CmsApi
{
    public partial class Article
    {
        public long Id { get; set; }

        [Required]
        public long AuthorId { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime? EditedAt { get; set; }
        public long? CreatedBy { get; set; }
        public long? EditedBy { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? PublishAt { get; set; }
        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public string Cover { get; set; }
        public string Tag { get; set; }
    }
}
