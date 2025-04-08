using System;

namespace Source_Demo.Models
{
    public class M_LibraryCategory : M_BaseModel.BaseCustom
    {
        public int id { get; set; }
        public string category_name { get; set; }
        public string description { get; set; }
        // Tùy vào logic, bạn có thể thêm/bớt trường, 
        // hoặc sử dụng create_at, update_at kế thừa từ BaseCustom nếu cần.
    }

    // Model mở rộng cho Validation
    public class EM_LibraryCategory : M_BaseModel.BaseCustom
    {
        public int id { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Vui lòng nhập tên danh mục")]
        [System.ComponentModel.DataAnnotations.StringLength(200, ErrorMessage = "Tên danh mục tối đa 200 ký tự")]
        public string category_name { get; set; }

        public string description { get; set; }
    }
}
