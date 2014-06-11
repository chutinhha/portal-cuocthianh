using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using BaseEntity;
namespace EntityData
{

    public interface IExamineeEntity : IEntityRepositoryBase<CoreData.Examinee>
    {
    }
    
    public class ExamineeEntity : EntityRepositoryBase<CoreData.Examinee>, IExamineeEntity
    {
    }
          
    
}
