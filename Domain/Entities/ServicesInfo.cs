//////////////////////////////////////////////////
//Author    : Mohammed Gaffer Aidaab
//For       : King Faisual University
//Under     : ISB integrated sulution business Company
// Services Management System 
//Date      : March - 2024 
/////////////////////////////////////////////////////

using Domain.Data.Enum;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities
{
    public class ServicesInfo
    {
        public int id { get; set; }
        
        [Required(ErrorMessage ="الرجاء ادخال اسم الخدمة")]
        [Display(Name="اسم الخدمة")]
        [MaxLength(120)]
        public string? Name { get; set; }

        [Required(ErrorMessage = "الرجاء ادخال نوع الخدمة")]
        [Display(Name="نوع الخدمة")]
        public ServicesTypes ServicesTypes { get; set; }

        [Display(Name = "رابط الخدمة على البيئة الانتاجية")]
        [MaxLength(120)]
        public string? Production_URL { get; set; }

        [Display(Name = "رابط الخدمة على البيئة التجريبية")]
        [MaxLength(120)]
        public string? Test_URL { get; set; }

        [Required(ErrorMessage = "الرجاء اختيار تصنيف الخدمة")]
        [Display(Name="التصنيف")]
        public ServicesCategories ServicesCategories { get; set; }

        [Required(ErrorMessage = "الرجاء تعبئة وصف الخدمة")]
        [Display(Name="وصف الخدمة")]
        [MaxLength(255)]
        public string? Description { get; set; }
        [NotMapped]
        [AllowNull]
        [Display(Name = "ملف التوثيق الخاص بالخدمة")]
        public IFormFile? File { get; set; }
        [AllowNull]
        public String? File_path { get; set; }
    }
}
