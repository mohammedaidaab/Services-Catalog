//////////////////////////////////////////////////
//Author    : Mohammed Gaffer Aidaab
//For       : King Faisual University
//Under     : ISB integrated sulution business Company
// Services Management System 
//Date      : March - 2024 
/////////////////////////////////////////////////////

using Domain.Entities;
using Domain.Interfaces;
using Serices_Applications.Data;
using Microsoft.AspNetCore.Identity;


namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRepository(ApplicationDbContext applicationDbContext,UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = applicationDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        } 

        public async Task<BaseResponse> CreateUser(AppUser? appUser)
        {
            if (await _userManager.FindByEmailAsync(appUser.Email) == null)
            {
                try
                {
                    var user = new IdentityUser()
                    {
                        UserName = appUser.Email,
                        Email = appUser.Email,
                        EmailConfirmed = appUser.EmailConfirmed,

                    };

                    var result = await _userManager.CreateAsync(user, appUser.Password);

                    if (result.Succeeded)
                    {
                        var role = _roleManager.FindByIdAsync(appUser.userrole);

                        await _userManager.AddToRoleAsync(user, role.Result.Name);
                        return new BaseResponse()
                        {
                            IsSuccess = true
                        };
                    }
                    else
                    {
                        return new BaseResponse() { IsSuccess = false, Message = "يرجى التحقق من قيود كلمة المرور " };

                    }

                }catch(Exception ex)
                {
                    return new BaseResponse()
                    {
                        IsSuccess = false,
                        Message = ex.Message,
                        
                    };
                }


            }
            else
            {
                return new BaseResponse()
                {
                    IsSuccess = false,
                    Message = "هذا المستخدم متواجد بافعل"
                    
                };
            }
        }

        public Task<BaseResponse> DeleteUser(AppUser user)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse> DisableUser(string id)
        {
            var user =  _context.Users.FirstOrDefault(x => x.Id == id);

            user.EmailConfirmed = false;

             _context.Update(user);

            await _context.SaveChangesAsync();
            
            return new BaseResponse() { IsSuccess = true};

        } 
        
        public async Task<BaseResponse> Aciveuser(string id)
        {
            var user =  _context.Users.FirstOrDefault(x => x.Id == id);

            user.EmailConfirmed = true;

             _context.Update(user);

            await _context.SaveChangesAsync();
            
            return new BaseResponse() { IsSuccess = true};

        }

        public async Task<IdentityUser> GetDetails(string Id)
        {

            var user = await _context.Users.FindAsync(Id);

            AppUser appUser = new()
            {
                Id = Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,

            };
            return appUser;

        }

        public async Task<BaseResponse> UpdateUser(string ID , AppUser user)
        {
            
            IdentityUser Dbuser = _context.Users.Find(ID);

            if (Dbuser != null)
            {
                Dbuser.UserName = user.UserName;
                Dbuser.Email = user.Email;
                Dbuser.PhoneNumber = user.PhoneNumber;

                _context.Users.Update(Dbuser);
                var result = _context.SaveChanges();

                if (result == 1)
                {
                    var userinrole = await _userManager.FindByIdAsync(user.Id);
                    var exrole = _userManager.GetRolesAsync(userinrole);
                    var newroles = _roleManager.FindByIdAsync(user.userrole);

                    try
                    {
                        foreach (var rol in exrole.Result)
                        {
                            var res0 = await _userManager.RemoveFromRoleAsync(userinrole, rol);

                        }
                        var res1 = await _userManager.AddToRoleAsync(userinrole, newroles.Result.Name);

                    }
                    catch (Exception ex)
                    {
                        ex.ToString();
                        
                    }

                }

            }

            return new BaseResponse() { IsSuccess=true };
        }
    }
}
