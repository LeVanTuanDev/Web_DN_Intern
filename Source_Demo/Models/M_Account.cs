using System.ComponentModel.DataAnnotations;

namespace Source_Demo.Models
{
    public class M_Account : M_BaseModel.BaseCustom
    {
        [Key]
        public string username { get; set; }
        public string pass_word { get; set; }
        public string avatar_url { get; set; }
        public int? employee_id { get; set; }
        public int? role_id { get; set; }
    }

    public class EM_Account : M_BaseModel.BaseCustom
    {
        [Required(ErrorMessage = "Vui lòng nhập tài khoản")]
        [StringLength(16, ErrorMessage = "Tài khoản có độ dài tối đa 16 ký tự")]
        public string username { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [StringLength(16, ErrorMessage = "Mật khẩu có độ dài tối đa 16 ký tự")]
        public string pass_word { get; set; }

        public string avatar_url { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn nhân viên")]
        public int employee_id { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn vai trò")]
        public int role_id { get; set; }
    }

    public class EM_LoginAccount : M_BaseModel.BaseCustom
    {
        [Required(ErrorMessage = "Vui lòng nhập tài khoản")]
        [StringLength(16, ErrorMessage = "Tài khoản có độ dài tối đa 16 ký tự")]
        public string username { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [StringLength(16, ErrorMessage = "Mật khẩu có độ dài tối đa 16 ký tự")]
        public string pass_word { get; set; }
    }
}
