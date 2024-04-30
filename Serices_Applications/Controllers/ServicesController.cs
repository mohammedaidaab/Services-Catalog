//////////////////////////////////////////////////
//Author    : Mohammed Gaffer Aidaab
//For       : King Faisual University
//Under     : ISB integrated sulution business Company
// Services Management System 
//Date      : March - 2024 
/////////////////////////////////////////////////////

using Domain.Data.Enum;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using NuGet.Protocol;
using System;
using UI.Extentions;

namespace UI.Controllers
{
    [Authorize]
    public class ServicesController : BaseController
    {

        private readonly IServicesRepository _ServicesRepository;

        public ServicesController(IServicesRepository servicesRepository)
        {
            _ServicesRepository = servicesRepository;
        }

        // GET: ServicesController
        [AllowAnonymous]
        public ActionResult Index(String? Filter)
        {
            
            FilterList(Filter);
            return View();
        }

        [AllowAnonymous]
        public IActionResult FilterList(String? Filter)
        {
            var services = _ServicesRepository.GetAll(Filter);
            return PartialView("Filter/FiltredServices",services);
        }

        // GET: ServicesController/Create
        [Authorize(Roles = "super_admin,admin,editor")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServicesController/Create
        [Authorize(Roles = "super_admin,admin,editor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ServicesInfo service)
        {
            var value = Enum.GetValues(typeof(ServicesTypes));
            

            try
            {
                BaseResponse result = await _ServicesRepository.CreateService(service);
                if (result.IsSuccess == true)
                {
                    BasicNotification("تم اضافة الخدمة بنجاح", NotificationType.Success);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction("create", service);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: ServicesController/Edit/5
        [Authorize(Roles = "super_admin,editor")]
        public async Task<ActionResult> Edit(int id)
        {
            var service  = await _ServicesRepository.GetById(id);

            return View(service);
        }

        // POST: ServicesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "super_admin,,admin,editor")]
        public async Task<ActionResult> Edit(int id, ServicesInfo service)
        {
            try
            {
                BaseResponse result = await _ServicesRepository.Update(service);
                if (result.IsSuccess == true) {
                    BasicNotification("تم تعديل البيانات بنجاح", NotificationType.Success);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    BasicNotification("هنالك خط في البيانات", NotificationType.Success);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View(service);
            }
        }

        // GET: ServicesController/Delete/5
        [Authorize(Roles = "super_admin,admin")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ServicesController/Delete/5
        [Authorize(Roles = "super_admin,admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
