using System.ComponentModel.DataAnnotations;

namespace Source_Demo.Models
{
    public class M_NewsManage : M_BaseModel.BaseCustom
    {
        public int news_id { get; set; }
        public int news_category_id { get; set; }
        public string news_name { get; set; }
        public string short_description { get; set; }
        public string content { get; set; }
        public int status_ { get; set; }
        public string name_slug { get; set; }

        // Các trường create_at, create_by, update_at, update_by kế thừa từ BaseCustom
    }

    public class EM_NewsManage : M_BaseModel.BaseCustom
    {
        [Required(ErrorMessage = "Vui lòng nhập mã tin tức")]
        public int news_id { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn danh mục tin tức")]
        public int news_category_id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên tin tức")]
        [StringLength(200, ErrorMessage = "Tên tin tức có độ dài tối đa 200 ký tự")]
        public string news_name { get; set; }

        [StringLength(500, ErrorMessage = "Mô tả ngắn có độ dài tối đa 500 ký tự")]
        public string short_description { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập nội dung")]
        public string content { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập trạng thái")]
        public int status_ { get; set; }

        [StringLength(200, ErrorMessage = "Slug có độ dài tối đa 200 ký tự")]
        public string name_slug { get; set; }

        // Các trường create_at, create_by, update_at, update_by kế thừa từ BaseCustom
    }
}
