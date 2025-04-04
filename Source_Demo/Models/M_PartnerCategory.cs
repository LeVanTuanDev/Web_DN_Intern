using System;
using System.ComponentModel.DataAnnotations;

namespace Source_Demo.Models
{
    public class M_PartnerCategory : M_BaseModel.BaseCustom
    {
        public int id { get; set; }
        public string category_name { get; set; }
        public string description { get; set; }
        public DateTime created_at { get; set; }

    }

    public class EM_PartnerCategory : M_BaseModel.BaseCustom
    {
        // Nếu id là tự tăng (IDENTITY) trong DB, 
        // bạn có thể không bắt buộc nhập trong form thêm mới
        [Required(ErrorMessage = "Vui lòng nhập ID")]
        public int id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên danh mục")]
        [StringLength(200, ErrorMessage = "Tên danh mục tối đa 200 ký tự")]
        public string category_name { get; set; }

        public string description { get; set; }

        public DateTime created_at { get; set; }
    }
}
