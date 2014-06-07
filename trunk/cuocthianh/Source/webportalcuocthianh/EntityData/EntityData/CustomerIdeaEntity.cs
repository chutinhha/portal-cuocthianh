using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using BaseEntity;
namespace EntityData
{

    public interface ICustomerIdeaEntity : IEntityRepositoryBase<CoreData.CustomerIdea>
    {
    }
    
    public class CustomerIdeaEntity : EntityRepositoryBase<CoreData.CustomerIdea>, ICustomerIdeaEntity
    {
    }
          
    
}
