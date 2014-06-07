using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using BaseEntity;
namespace EntityData
{

    public interface IEmailListEntity : IEntityRepositoryBase<CoreData.EmailList>
    {
    }
    
    public class EmailListEntity : EntityRepositoryBase<CoreData.EmailList>, IEmailListEntity
    {
    }
          
    
}
