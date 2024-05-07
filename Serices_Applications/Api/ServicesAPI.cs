//////////////////////////////////////////////////
//Author    : Mohammed Gaffer Aidaab
//For       : King Faisual University
//Under     : ISB integrated sulution business Company
// Services Management System 
//Date      : March - 2024 
/////////////////////////////////////////////////////

using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UI.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesAPI : ControllerBase
    {
        private readonly IServicesRepository _ServicesRepository;

        public ServicesAPI(IServicesRepository servicesRepository)
        {
            _ServicesRepository = servicesRepository;
        }

        [HttpPost]
        public async Task <IActionResult> GetAll(String? Filter)
        {
            var data = _ServicesRepository.GetAll(Filter);
            return Ok(data);
        }

    }
}
