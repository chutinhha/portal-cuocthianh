using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using BaseEntity;
namespace EntityData
{

    public interface IUser_Role_ModuleEntity : IEntityRepositoryBase<CoreData.User_Role_Module>
    {
    }
    
    public class User_Role_ModuleEntity : EntityRepositoryBase<CoreData.User_Role_Module>, IUser_Role_ModuleEntity
    {
    }
          
    
}
