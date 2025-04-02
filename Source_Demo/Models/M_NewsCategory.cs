using System.ComponentModel.DataAnnotations;

namespace Source_Demo.Models
{
    public class M_NewsCategory : M_BaseModel.BaseCustom
    {
        public int news_category_id { get; set; }
        public string news_category_name { get; set; }
        public int status_ { get; set; }

        // Các trường create_at, create_by, update_at, update_by kế thừa từ BaseCustom
    }

    public class EM_NewsCategory : M_BaseModel.BaseCustom
    {
        [Required(ErrorMessage = "Vui lòng nhập mã danh mục tin tức")]
        public int news_category_id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên danh mục tin tức")]
        [StringLength(100, ErrorMessage = "Tên danh mục tin tức có độ dài tối đa 100 ký tự")]
        public string news_category_name { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập trạng thái")]
        public int status_ { get; set; }

        // Các trường create_at, create_by, update_at, update_by kế thừa từ BaseCustom
    }
}
