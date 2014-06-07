using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using BaseEntity;
namespace EntityData
{

    public interface IBanner_LogoEntity : IEntityRepositoryBase<CoreData.Banner_Logo>
    {
    }
    
    public class Banner_LogoEntity : EntityRepositoryBase<CoreData.Banner_Logo>, IBanner_LogoEntity
    {
    }
          
    
}
