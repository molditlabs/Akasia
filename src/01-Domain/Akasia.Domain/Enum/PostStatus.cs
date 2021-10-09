using System;
using System.Collections.Generic;
using System.Text;

namespace Akasia.Domain.Enum
{
    public enum PostStatus : int
    {
        Draft = 10,     // IsDeleted = false
        Published = 20, // IsDeleted = false
        Trash = 30,     // IsDeleted = true, Can be restored
        Deleted = 40    // IsDeleted = true, Can not be restored
    }
}
