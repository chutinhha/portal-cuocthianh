using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using BaseEntity;
namespace EntityData
{

    public interface IProductEntity : IEntityRepositoryBase<CoreData.Product>
    {
    }
    
    public class ProductEntity : EntityRepositoryBase<CoreData.Product>, IProductEntity
    {
    }
          
    
}
