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
using Serices_Applications.Data;
using System.Security.Policy;

namespace Infrastructure.Repositories
{
    public class SerevicesRepository : IServicesRepository
    {
        private readonly ApplicationDbContext _context;

        public SerevicesRepository(ApplicationDbContext applicationDbContext)
        {
                _context = applicationDbContext;
        }
        //adding new serice 
        public async Task<BaseResponse> CreateService(ServicesInfo service)
        {
            try
            {
                if (service.File != null)
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Attachments/ServicesDocumentations");
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    string fileNameWithPath = Path.Combine(path, service.File.FileName);
                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        service.File.CopyTo(stream);
                    }
                    string filepath = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\Attachments\ServicesDocumentations" + service.File.FileName}";

                    service.File_path = service.File.FileName;

                }
                else
                {
                    service.File_path = null;
                }

                _context.servicesInfos.Add(service);
                await _context.SaveChangesAsync();

                return new BaseResponse()
                {
                    IsSuccess = true,
                    Message = "the data was saved successfully",
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }
        //get all services 
        public IEnumerable<ServicesInfo> GetAll(String? Filter)
        {

            var allservies = _context.servicesInfos.ToList();
            List<ServicesInfo> disList = new List<ServicesInfo>();
            if (Filter != null )
            {
                ServicesCategories stype = (ServicesCategories)Enum.Parse(typeof(ServicesCategories), Filter);
                foreach (var service in allservies)
                {
                    if (service.ServicesCategories == stype)
                    {
                        disList.Add(service);
                    }
                }
                return disList;
            }
            return allservies;
            
        }

        //get service by id
        public async Task<ServicesInfo> GetById(int id)
        {
            ServicesInfo? service =  await _context.servicesInfos.FindAsync(id);

            return (service);
        }
        //update service information 
        public async Task<BaseResponse> Update(ServicesInfo service)
        {
            var DbService = _context.servicesInfos.FirstOrDefault(x => x.id == service.id);


            //check if file is changed with new one 
            if (service.File != null)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Attachments/ServicesDocumentations");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                string fileNameWithPath = Path.Combine(path, service.File.FileName);
                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    service.File.CopyTo(stream);
                }
                string filepath = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\Attachments\ServicesDocumentations" + service.File.FileName}";

                service.File_path = service.File.FileName;

            }
            else
            {
                // keep the same file path 
                service.File_path = DbService.File_path;
            }


            //update the service parameters properties 
            if (DbService != null)
            {                
                _context.Entry(DbService).CurrentValues.SetValues(service);
                await _context.SaveChangesAsync();

                return new BaseResponse()
                {
                    IsSuccess = true,
                    Message = "service added successfully",
                };
            }
            else {
                return new BaseResponse()
                {
                    IsSuccess = false,
                    Message = "there an error cant adding the service ",
                };
            }
        }
    }
}
