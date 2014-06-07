using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using BaseEntity;
namespace EntityData
{

    public interface IDistrictEntity : IEntityRepositoryBase<CoreData.District>
    {
    }
    
    public class DistrictEntity : EntityRepositoryBase<CoreData.District>, IDistrictEntity
    {
    }
          
    
}
