using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using BaseEntity;
namespace EntityData
{

    public interface IProductAttributeEntity : IEntityRepositoryBase<CoreData.ProductAttribute>
    {
    }
    
    public class ProductAttributeEntity : EntityRepositoryBase<CoreData.ProductAttribute>, IProductAttributeEntity
    {
    }
          
    
}
