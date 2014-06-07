using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using BaseEntity;
namespace EntityData
{

    public interface IProductTypeEntity : IEntityRepositoryBase<CoreData.ProductType>
    {
    }
    
    public class ProductTypeEntity : EntityRepositoryBase<CoreData.ProductType>, IProductTypeEntity
    {
    }
          
    
}
