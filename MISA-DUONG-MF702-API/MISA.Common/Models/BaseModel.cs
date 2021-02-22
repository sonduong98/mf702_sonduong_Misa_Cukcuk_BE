using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Common.Models
{
    public class BaseModel
    {
        public int EmployeeId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string CreatedBy { get; set; }

    }
}
