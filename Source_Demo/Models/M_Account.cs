using System.ComponentModel.DataAnnotations;

namespace Source_Demo.Models
{
    public class M_Account : M_BaseModel.BaseCustom
    {
        public int id { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string accessToken { get; set; }
    }
    public class EM_Account : M_BaseModel.BaseCustom
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tài khoản")]
        [StringLength(16, ErrorMessage = "Tài khoản có độ dài tối đa 16 ký tự")]
        public string userName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [StringLength(16, ErrorMessage = "Mật khẩu có độ dài tối đa 16 ký tự")]
        public string password { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ")]
        [StringLength(10, ErrorMessage = "Họ có độ dài tối đa 10 ký tự")]
        public string lastName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên")]
        [StringLength(10, ErrorMessage = "Tên có độ dài tối đa 10 ký tự")]
        public string firstName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email")]
        [StringLength(32, ErrorMessage = "Email có độ dài tối đa 32 ký tự")]
        [RegularExpression(@"^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$",
            ErrorMessage = "Email không hợp lệ")]
        public string email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [StringLength(16, ErrorMessage = "Điện thoại có độ dài tối đa 16 ký tự")]
        [RegularExpression(@"^\d{10,11}$", ErrorMessage = "Số điện thoại không hợp lệ")]
        public string phone { get; set; }
    }

    public class EM_LoginAccount : M_BaseModel.BaseCustom
    {
        [Required(ErrorMessage = "Vui lòng nhập tài khoản")]
        [StringLength(16, ErrorMessage = "Tài khoản có độ dài tối đa 16 ký tự")]
        public string userName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [StringLength(16, ErrorMessage = "Mật khẩu có độ dài tối đa 16 ký tự")]
        public string password { get; set; }
    }
}
