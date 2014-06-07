using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using BaseEntity;
namespace EntityData
{

    public interface IEmailTemplateEntity : IEntityRepositoryBase<CoreData.EmailTemplate>
    {
    }
    
    public class EmailTemplateEntity : EntityRepositoryBase<CoreData.EmailTemplate>, IEmailTemplateEntity
    {
    }
          
    
}
