using System.ComponentModel.DataAnnotations;

namespace Shipping.DTO.Employee_DTOs
{
    public class EmpDTO
    {
        public string? id { get; set; }
        [Required(ErrorMessage = "يجب ادخال الاسم")]
        public string name { get; set; }
        [Required(ErrorMessage = "يجب ادخال البريد الالكتروني")]
        [RegularExpression("^\\S+@\\S+\\.\\S+$", ErrorMessage = "ادخل بريد الكتروني صحيح")]
        public string email { get; set; }
        [Required(ErrorMessage = "يجب ادخال رقم الهاتف")]
        [RegularExpression(@"^01[0125][0-9]{8}$", ErrorMessage = "ادخل رقم هاتف صحيح كالاتى : 01224479550")]
        public string phone { get; set; }
        public bool status { get; set; } = true;
        public string? branchName { get; set; }
        public int? branchId { get; set; }
        [Required(ErrorMessage = "يجب ادخال الصلاحية")]
        public string role { get; set; }
        [StringLength(255, ErrorMessage = "ادخل ع الاقل 8احرف و ارقام", MinimumLength = 8)]
        public string? password { get; set; }
    }
}
