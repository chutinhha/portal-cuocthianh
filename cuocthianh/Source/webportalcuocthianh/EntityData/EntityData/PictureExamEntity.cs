using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using BaseEntity;
namespace EntityData
{

    public interface IPictureExamEntity : IEntityRepositoryBase<CoreData.PictureExam>
    {
    }
    
    public class PictureExamEntity : EntityRepositoryBase<CoreData.PictureExam>, IPictureExamEntity
    {
    }
          
    
}
