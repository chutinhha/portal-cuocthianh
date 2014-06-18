using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using BaseEntity;
namespace EntityData
{

    public interface ICategoryEntity : IEntityRepositoryBase<CoreData.Category>
    {
    }
    
    public class CategoryEntity : EntityRepositoryBase<CoreData.Category>, ICategoryEntity
    {
    }
          
    
}