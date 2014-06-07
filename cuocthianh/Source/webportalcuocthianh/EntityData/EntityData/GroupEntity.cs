using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using BaseEntity;
namespace EntityData
{

    public interface IGroupEntity : IEntityRepositoryBase<CoreData.Group>
    {
    }
    
    public class GroupEntity : EntityRepositoryBase<CoreData.Group>, IGroupEntity
    {
    }
          
    
}
