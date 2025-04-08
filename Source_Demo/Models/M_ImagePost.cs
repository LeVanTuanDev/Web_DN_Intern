using System;

namespace Source_Demo.Models
{
    public class M_ImagePost : M_BaseModel.BaseCustom
    {
        public int id { get; set; }
        public int post_id { get; set; }
        public string image_url { get; set; }
        public bool is_avatar { get; set; }
        public DateTime create_at { get; set; }

        // Các trường create_at, create_by, update_at, update_by 
        // nếu đã có trong BaseCustom, bạn có thể tùy chỉnh lại 
        // hoặc bỏ bớt ở đây cho phù hợp.
    }

    // Model mở rộng cho Validation
    public class EM_ImagePost : M_BaseModel.BaseCustom
    {
        public int id { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Vui lòng nhập post_id")]
        public int post_id { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Vui lòng nhập đường dẫn ảnh")]
        public string image_url { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Vui lòng xác định ảnh đại diện")]
        public bool is_avatar { get; set; }

        public DateTime create_at { get; set; }
    }
}
