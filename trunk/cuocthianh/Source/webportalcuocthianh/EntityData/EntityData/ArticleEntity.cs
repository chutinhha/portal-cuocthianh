using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using BaseEntity;
namespace EntityData
{
    public interface IArticleEntity : IEntityRepositoryBase<CoreData.Article>
    {
    }

    public class ArticleEntity : EntityRepositoryBase<CoreData.Article>, IArticleEntity
    {
    }
}
