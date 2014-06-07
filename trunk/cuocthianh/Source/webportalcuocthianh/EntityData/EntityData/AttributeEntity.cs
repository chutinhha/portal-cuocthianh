using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using BaseEntity;
namespace EntityData
{

    public interface IAttributeEntity : IEntityRepositoryBase<CoreData.Attribute>
    {
    }
    
    public class AttributeEntity : EntityRepositoryBase<CoreData.Attribute>, IAttributeEntity
    {
    }
         
}
