using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using BaseEntity;
namespace EntityData
{

    public interface ICommentEntity : IEntityRepositoryBase<CoreData.Comment>
    {
    }
    
    public class CommentEntity : EntityRepositoryBase<CoreData.Comment>, ICommentEntity
    {
    }
          
    
}
