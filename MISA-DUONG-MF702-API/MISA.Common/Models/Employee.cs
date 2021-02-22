using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MISA.Common.Models
{
    public partial class Employee :BaseModel
    {
        
        public string EmployeeCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [StringLength(512, ErrorMessage = "Họ tên phải ngắn hơn 250 kí tự")]
        public string FullName { get; set; }
        public int? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string CitizenIdentityCode { get; set; }
        public string CitizebIdentityPlace { get; set; }
        public DateTime? CitizebIdentityDate { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string WorkState { get; set; }
        public int? PositionId { get; set; }
        public int? DepartmentId { get; set; }
        public string SelfTaxCode { get; set; }
        public double? Salary { get; set; }
        
    }
}
