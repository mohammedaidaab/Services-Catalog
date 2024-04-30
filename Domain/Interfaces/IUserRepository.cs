//////////////////////////////////////////////////
//Author    : Mohammed Gaffer Aidaab
//For       : King Faisual University
//Under     : ISB integrated sulution business Company
// Services Management System 
//Date      : March - 2024 
/////////////////////////////////////////////////////

using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        public Task<BaseResponse> CreateUser(AppUser appUser);
        public Task<BaseResponse> UpdateUser(string Id,AppUser user);
        public Task<BaseResponse> DeleteUser(AppUser user);
        public Task<IdentityUser> GetDetails(string Id);
        public Task<BaseResponse> DisableUser(string id);
        public Task<BaseResponse> Aciveuser (string id);
    }
}
