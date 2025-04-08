using System;

namespace Source_Demo.Models
{
    public class M_LibraryManage : M_BaseModel.BaseCustom
    {
        public int id { get; set; }
        public string title { get; set; }
        public string library_description { get; set; }
        public int category_id { get; set; }
        public DateTime created_at { get; set; }
    }

    // Model mở rộng cho Validation
    public class EM_LibraryManage : M_BaseModel.BaseCustom
    {
        public int id { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Vui lòng nhập tiêu đề thư viện")]
        [System.ComponentModel.DataAnnotations.StringLength(200, ErrorMessage = "Tiêu đề tối đa 200 ký tự")]
        public string title { get; set; }

        public string library_description { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Vui lòng chọn danh mục")]
        public int category_id { get; set; }

        public DateTime created_at { get; set; }
    }
}
