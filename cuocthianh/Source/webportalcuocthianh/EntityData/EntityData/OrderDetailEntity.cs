using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using BaseEntity;
namespace EntityData
{

    public interface IOrderDetailEntity : IEntityRepositoryBase<CoreData.OrderDetail>
    {
    }
    
    public class OrderDetailEntity : EntityRepositoryBase<CoreData.OrderDetail>, IOrderDetailEntity
    {
    }
          
    
}
