//////////////////////////////////////////////////
//Author    : Mohammed Gaffer Aidaab
//For       : King Faisual University
//Under     : ISB integrated sulution business Company
// Services Management System 
//Date      : March - 2024 
/////////////////////////////////////////////////////

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class AppUser : IdentityUser
    {
        [Display(Name = "تنشيط المستخدم")]
        public bool IsActive { get; set; }
        [Display(Name = "البريد الاكتروني")]
        public string? Email { get; set; }
        [Display(Name = "كلمة المرور")]
        public string? Password { get; set; }
        [Display(Name ="صلاحية المستخدم في النظام")]
        public string? userrole { get; set; }
    }
}
