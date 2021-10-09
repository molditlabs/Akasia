using System;
using System.Collections.Generic;
using System.Text;

namespace Akasia.Domain.Entity.Base
{
    public class BaseModel
    {
        // Key
        public int Id { get; set; }

        // Row Attribute
        public bool IsDeleted { get; set; }


        // Audit Fields
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
