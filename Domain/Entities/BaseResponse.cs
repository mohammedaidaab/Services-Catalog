//////////////////////////////////////////////////
//Author    : Mohammed Gaffer Aidaab
//For       : King Faisual University
//Under     : ISB integrated sulution business Company
// Services Management System 
//Date      : March - 2024 
/////////////////////////////////////////////////////

namespace Domain.Entities
{
    public class BaseResponse
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
    }
}
