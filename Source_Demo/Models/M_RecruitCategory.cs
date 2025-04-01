using System.ComponentModel.DataAnnotations;

namespace Source_Demo.Models
{
    public class M_RecruitCategory : M_BaseModel.BaseCustom
    {
        public int recruitment_id { get; set; }
        public string recruitment_name { get; set; }
        public int business_id { get; set; }
        public string recruitment_position { get; set; }
        public string content { get; set; }
        // Các trường create_at, create_by, update_at, update_by kế thừa từ BaseCustom
    }

    public class EM_RecruitCategory : M_BaseModel.BaseCustom
    {
        [Required(ErrorMessage = "Vui lòng nhập mã tuyển dụng")]
        public int recruitment_id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên tuyển dụng")]
        [StringLength(100, ErrorMessage = "Tên tuyển dụng có độ dài tối đa 100 ký tự")]
        public string recruitment_name { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn mã doanh nghiệp")]
        public int business_id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập vị trí tuyển dụng")]
        [StringLength(100, ErrorMessage = "Vị trí tuyển dụng có độ dài tối đa 100 ký tự")]
        public string recruitment_position { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập nội dung")]
        public string content { get; set; }
        // Các trường create_at, create_by, update_at, update_by kế thừa từ BaseCustom
    }
}
