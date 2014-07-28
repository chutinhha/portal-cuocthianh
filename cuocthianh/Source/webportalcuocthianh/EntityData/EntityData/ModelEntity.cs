using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using BaseEntity;
namespace EntityData
{

    public interface IModelEntity : IEntityRepositoryBase<CoreData.Model>
    {
    }
    
    public class ModelEntity : EntityRepositoryBase<CoreData.Model>, IModelEntity
    {
    }
          
    
}