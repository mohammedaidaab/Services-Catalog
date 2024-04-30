//////////////////////////////////////////////////
//Author    : Mohammed Gaffer Aidaab
//For       : King Faisual University
//Under     : ISB integrated sulution business Company
// Services Management System 
//Date      : March - 2024 
/////////////////////////////////////////////////////

using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class AppRoles : IdentityRole
    {
        public string? Description { get; set; }
    }
}
