using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using BaseEntity;
namespace EntityData
{

    public interface IConfigurationEntity : IEntityRepositoryBase<CoreData.Configuration>
    {
    }
    
    public class ConfigurationEntity : EntityRepositoryBase<CoreData.Configuration>, IConfigurationEntity
    {
    }
          
    
}
