using Akasia.Domain.Entity.Base;
using Akasia.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Akasia.Domain.Entity
{
    public class BlogPost : BaseModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PostDate { get; set; }        
        public PostStatus Status { get; set; }
    }
}
