using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using BaseEntity;
namespace EntityData
{

    public interface IModuleEntity : IEntityRepositoryBase<CoreData.Module>
    {
    }
    
    public class ModuleEntity : EntityRepositoryBase<CoreData.Module>, IModuleEntity
    {
    }
          
    
}
