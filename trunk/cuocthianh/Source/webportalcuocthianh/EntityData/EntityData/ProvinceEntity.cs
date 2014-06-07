using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using BaseEntity;
namespace EntityData
{

    public interface IProvinceEntity : IEntityRepositoryBase<CoreData.Province>
    {
    }
    
    public class ProvinceEntity : EntityRepositoryBase<CoreData.Province>, IProvinceEntity
    {
    }
          
    
}
