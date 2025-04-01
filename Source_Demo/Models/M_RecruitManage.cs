using System.ComponentModel.DataAnnotations;

namespace Source_Demo.Models
{
    public class M_RecruitManage : M_BaseModel.BaseCustom
    {
        public int application_id { get; set; }
        public int recruitment_id { get; set; }
        public string full_name { get; set; }
        public string education_level { get; set; }
        public string professional_qualifications { get; set; }
        public string cv_url { get; set; }
        // Các trường create_at, create_by, update_at, update_by sẽ được kế thừa từ BaseCustom (nếu có).
    }

    public class EM_RecruitManage : M_BaseModel.BaseCustom
    {
        [Required(ErrorMessage = "Vui lòng nhập mã Application")]
        public int application_id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mã tuyển dụng")]
        public int recruitment_id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ và tên")]
        [StringLength(100, ErrorMessage = "Họ và tên có độ dài tối đa 100 ký tự")]
        public string full_name { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập trình độ học vấn")]
        [StringLength(100, ErrorMessage = "Trình độ học vấn có độ dài tối đa 100 ký tự")]
        public string education_level { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập trình độ chuyên môn")]
        [StringLength(200, ErrorMessage = "Trình độ chuyên môn có độ dài tối đa 200 ký tự")]
        public string professional_qualifications { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập đường dẫn CV")]
        [StringLength(300, ErrorMessage = "Đường dẫn CV có độ dài tối đa 300 ký tự")]
        public string cv_url { get; set; }
        // Các trường create_at, create_by, update_at, update_by sẽ được kế thừa từ BaseCustom (nếu có).
    }
}
