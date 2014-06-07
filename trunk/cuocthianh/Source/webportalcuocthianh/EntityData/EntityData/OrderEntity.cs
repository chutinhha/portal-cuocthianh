using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using BaseEntity;
namespace EntityData
{

    public interface IOrderEntity : IEntityRepositoryBase<CoreData.Order>
    {
    }
    
    public class OrderEntity : EntityRepositoryBase<CoreData.Order>, IOrderEntity
    {
    }
          
    
}
