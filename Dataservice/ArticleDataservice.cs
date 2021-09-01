using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Homo.CmsApi
{
    public class ArticleDataservice
    {
        public static Article GetOne(CmsDbContext dbContext, long id, bool asNoTracking = false)
        {
            DbSet<Article> dbSet = dbContext.Article;
            IQueryable<Article> queryablArticle = dbSet;
            if (asNoTracking)
            {
                queryablArticle = queryablArticle.AsNoTracking();
            }
            return queryablArticle.Where(x => x.Id == id && x.DeletedAt == null).FirstOrDefault();
        }

        public static List<Article> GetList(CmsDbContext dbContext, int page, int limit, string title)
        {
            return dbContext.Article
                .Where(x =>
                    x.DeletedAt == null
                    && (title == null || x.Title.Contains(title))
                )
                .OrderByDescending(x => x.Id)
                .Skip(limit * (page - 1))
                .Take(limit)
                .ToList();
        }

        public static List<Article> GetAll(CmsDbContext dbContext, string title)
        {
            return dbContext.Article
                .Where(x =>
                    x.DeletedAt == null
                    && (title == null || x.Title.Contains(title))
                )
                .OrderByDescending(x => x.Id)
                .ToList();
        }
        public static int GetRowNum(CmsDbContext dbContext, string title)
        {
            return dbContext.Article
                .Where(x =>
                    x.DeletedAt == null
                    && (title == null || x.Title.Contains(title))
                )
                .Count();
        }

        public static Article GetOne(CmsDbContext dbContext, long id)
        {
            return dbContext.Article.FirstOrDefault(x => x.DeletedAt == null && x.Id == id);
        }

        public static Article Create(CmsDbContext dbContext, long createdBy, DTOs.Article dto)
        {
            Article record = new Article();
            foreach (var propOfDTO in dto.GetType().GetProperties())
            {
                var value = propOfDTO.GetValue(dto);
                var prop = record.GetType().GetProperty(propOfDTO.Name);
                prop.SetValue(record, value);
            }
            record.CreatedBy = createdBy;
            dbContext.Article.Add(record);
            dbContext.SaveChanges();
            return record;
        }

        public static int BatchDelete(CmsDbContext dbContext, long editedBy, List<long?> ids)
        {
            foreach (int id in ids)
            {
                Article record = new Article { Id = id };
                dbContext.Attach<Article>(record);
                record.DeletedAt = DateTime.Now;
                record.EditedBy = editedBy;
            }
            return dbContext.SaveChanges();
        }

        public static void Update(CmsDbContext dbContext, int id, long editedBy, DTOs.Article dto)
        {
            Article record = dbContext.Article.Where(x => x.Id == id).FirstOrDefault();
            foreach (var propOfDTO in dto.GetType().GetProperties())
            {
                var value = propOfDTO.GetValue(dto);
                var prop = record.GetType().GetProperty(propOfDTO.Name);
                prop.SetValue(record, value);
            }
            record.EditedAt = DateTime.Now;
            record.EditedBy = editedBy;
            dbContext.SaveChanges();
        }

        public static void Delete(CmsDbContext dbContext, long id, long editedBy)
        {
            Article record = dbContext.Article.Where(x => x.Id == id).FirstOrDefault();
            record.DeletedAt = DateTime.Now;
            record.EditedBy = editedBy;
            dbContext.SaveChanges();
        }
    }
}