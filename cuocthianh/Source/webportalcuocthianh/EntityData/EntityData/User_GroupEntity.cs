using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using BaseEntity;
namespace EntityData
{

    public interface IUser_GroupEntity : IEntityRepositoryBase<CoreData.User_Group>
    {
    }
    
    public class User_GroupEntity : EntityRepositoryBase<CoreData.User_Group>, IUser_GroupEntity
    {
    }
          
    
}
