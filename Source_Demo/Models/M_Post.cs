using System;

namespace Source_Demo.Models
{
    public class M_Post : M_BaseModel.BaseCustom
    {
        public int post_id { get; set; }
        public string title { get; set; }
        public string short_description { get; set; }
        public string content { get; set; }
        public int field_id { get; set; }
        public int view_count { get; set; }
        public string username { get; set; }
        public int status { get; set; }
        public string name_slug { get; set; }
        public DateTime create_at { get; set; }
        public int create_by { get; set; }
        public DateTime update_at { get; set; }
        public int update_by { get; set; }
    }

    // Model mở rộng cho Validation
    public class EM_Post : M_BaseModel.BaseCustom
    {
        public int post_id { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Vui lòng nhập tiêu đề")]
        [System.ComponentModel.DataAnnotations.StringLength(200, ErrorMessage = "Tiêu đề tối đa 200 ký tự")]
        public string title { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(500, ErrorMessage = "Mô tả ngắn tối đa 500 ký tự")]
        public string short_description { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Vui lòng nhập nội dung")]
        public string content { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Vui lòng chọn lĩnh vực (field_id)")]
        public int field_id { get; set; }

        public int view_count { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Vui lòng nhập tên tài khoản")]
        public string username { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Vui lòng nhập trạng thái")]
        public int status { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(200, ErrorMessage = "Slug tối đa 200 ký tự")]
        public string name_slug { get; set; }

        public DateTime create_at { get; set; }
        public int create_by { get; set; }
        public DateTime update_at { get; set; }
        public int update_by { get; set; }
    }
}
