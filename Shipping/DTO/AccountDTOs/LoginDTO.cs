using System.ComponentModel.DataAnnotations;

namespace Shipping.DTO.AccountDTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "يجب ادخال البريد الالكتروني او اسم المستخدم")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "يجب ادخال الرقم السري")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
