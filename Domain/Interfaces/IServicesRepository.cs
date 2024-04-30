//////////////////////////////////////////////////
//Author    : Mohammed Gaffer Aidaab
//For       : King Faisual University
//Under     : ISB integrated sulution business Company
// Services Management System 
//Date      : March - 2024 
/////////////////////////////////////////////////////
using Domain.Data.Enum;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IServicesRepository
    {
        //Get All Services 
        public IEnumerable<ServicesInfo> GetAll(String Filter);
        //Create service 
        public Task<BaseResponse> CreateService(ServicesInfo service);
        //Get by Id
        public Task<ServicesInfo> GetById(int id);
        //update user data
        public Task<BaseResponse> Update(ServicesInfo service);
    }
}
