using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using BaseEntity;
namespace EntityData
{

    public interface IGroup_Role_ModuleEntity : IEntityRepositoryBase<CoreData.Group_Role_Module>
    {
    }
    
    public class Group_Role_ModuleEntity : EntityRepositoryBase<CoreData.Group_Role_Module>, IGroup_Role_ModuleEntity
    {
    }
          
    
}
