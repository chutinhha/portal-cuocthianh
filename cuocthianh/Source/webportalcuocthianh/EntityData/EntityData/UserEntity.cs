using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using BaseEntity;
namespace EntityData
{

    public interface IUserEntity : IEntityRepositoryBase<CoreData.User>
    {
    }
    
    public class UserEntity : EntityRepositoryBase<CoreData.User>, IUserEntity
    {
    }
          
    
}
