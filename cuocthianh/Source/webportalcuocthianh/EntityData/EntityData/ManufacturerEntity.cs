using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using BaseEntity;
namespace EntityData
{

    public interface IManufacturerEntity : IEntityRepositoryBase<CoreData.Manufacturer>
    {
    }
    
    public class ManufacturerEntity : EntityRepositoryBase<CoreData.Manufacturer>, IManufacturerEntity
    {
    }
          
    
}
