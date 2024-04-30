//////////////////////////////////////////////////
//Author    : Mohammed Gaffer Aidaab
//For       : King Faisual University
//Under     : ISB integrated sulution business Company
// Services Management System 
//Date      : March - 2024 
/////////////////////////////////////////////////////

using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Serices_Applications.Data;
using UI.Extentions;

namespace UI.Controllers
{
    [Authorize]
	public class UsersController : BaseController
	{
		private readonly ApplicationDbContext _context;
		private readonly IUserRepository _userRepository;

        public UsersController(ApplicationDbContext context,IUserRepository userRepository)
        {
			_context = context;
			_userRepository = userRepository;
        }
		// GET: Users
		[Authorize(Roles ="super_admin,admin")]
        public ActionResult index()
		{
			return View( _context.Users. ToList());
            //var usersWithRoles = (from user in _context.Users
            //                      select new
            //                      {
            //                          Username = user.UserName,
            //                          Email = user.Email,
            //                          RoleNames = (from userRole in user.Roles
            //                                       join role in _context.Roles on userRole.RoleId equals role.Id
            //                                       select role.Name).ToList()
            //                      })
            //                .ToList()
            //                .Select(p => new AppUser
            //                {
            //                    UserName = p.Username,
            //                    Email = p.Email,
            //                    userrole = string.Join(",", p.RoleNames)
            //                });
        }

        // GET: Users/Details/5
        [Authorize(Roles = "super_admin,admin")]
        public async Task<ActionResult> Details(string id)
		{
			var user = await _userRepository.GetDetails(id);
			return View(user);
		}

        // GET: Users/Create
        [Authorize(Roles = "super_admin,admin")]
        public ActionResult Create()
		{
            ViewBag.Roles = new SelectList(_context.Roles.Where(x=>x.Name != "super_admin"), nameof(IdentityRole.Id), nameof(IdentityRole.Name));
            
            return View();
		}

        // POST: Users/Create
        [HttpPost]
        [Authorize(Roles = "super_admin,admin")]
        [ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(AppUser appUser)
		{
			try
			{
				BaseResponse res = await _userRepository.CreateUser(appUser);
                if(res.IsSuccess == true)
                {
                    BasicNotification("تم اضافة المستخدم بنجاح", NotificationType.Success);
                    return RedirectToAction(nameof(index));
                }
                else
                {
                    BasicNotification(res.Message,NotificationType.Error);
                    return RedirectToAction("create",appUser);
                }
				
			}
			catch(Exception ex)
			{
				return RedirectToAction("create");
			}
		}

        // GET: Users/Edit/5
        [Authorize(Roles = "super_admin,admin")]
        public async Task<ActionResult> Edit(string? id)
		{
			if (id != null)
			{
                IdentityUser user = await _userRepository.GetDetails(id);

                ViewBag.Roles = new SelectList(_context.Roles.Where(x => x.Name != "super_admin"), nameof(IdentityRole.Id), nameof(IdentityRole.Name));

                return View(user);
            }
			else
			{
                TempData["message"] = "الرجاء إختيار المستخدم ";
                return RedirectToAction("index");
                
            }
			
		}

		// POST: Users/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
        [Authorize(Roles = "super_admin,admin")]
        public async Task<ActionResult> Edit(string id, AppUser collection)
		{
			try
			{
				var result = await _userRepository.UpdateUser(id, collection);
				if (result.IsSuccess == true) {
                    BasicNotification("تم تعديل بيانات المستخدم بنجاح", NotificationType.Success);
                    return RedirectToAction(nameof(index));
                }
                return View(collection);

            }
			catch(Exception ex)
			{
				return View(ex.ToString());
			}
		}

        // GET: Users/Delete/5
        [Authorize(Roles = "super_admin,admin")]
        public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: Users/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
        [Authorize(Roles = "super_admin,admin")]
        public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(index));
			}
			catch
			{
				return View();
			}
		}

		public async Task<ActionResult> Disableuser(string id)
		{
			BaseResponse res = await _userRepository.DisableUser(id);
			if (res.IsSuccess == true) {
                BasicNotification("تم الغاء تفعيل المستخدم بنجاح", NotificationType.Info);
                return RedirectToAction("index");
			}
			else
			{
                BasicNotification("الرجاءالتحقق من العملية", NotificationType.Success);
                return Redirect("index");
            }
			
		}

        public async Task<ActionResult> Aciveuser(string id)
        {
            BaseResponse res = await _userRepository.Aciveuser(id);
            if (res.IsSuccess == true)
            {
                BasicNotification("تم تفعيل المستخدم بنجاح", NotificationType.Success);
                return RedirectToAction("index");
            }
            else
            {
                BasicNotification("يوجد خط االرجاء التحقق", NotificationType.Success);
                return Redirect("index");
            }

        }
    }
}
