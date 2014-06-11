using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using BaseEntity;
namespace EntityData
{

    public interface ICategoryArticleEntity : IEntityRepositoryBase<CoreData.CategoryArticle>
    {
    }
    
    public class CategoryArticleEntity : EntityRepositoryBase<CoreData.CategoryArticle>, ICategoryArticleEntity
    {
    }
          
    
}
