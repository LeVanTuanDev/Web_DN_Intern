using System;

namespace Source_Demo.Models
{
    public class M_ImageNews : M_BaseModel.BaseCustom
    {
        public int id { get; set; }
        public int news_id { get; set; }
        public string image_url { get; set; }
        public bool is_avatar { get; set; }
        public DateTime create_at { get; set; }
    }

    // Model mở rộng cho Validation
    public class EM_ImageNews : M_BaseModel.BaseCustom
    {
        public int id { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Vui lòng nhập mã tin tức")]
        public int news_id { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Vui lòng nhập đường dẫn ảnh")]
        public string image_url { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Vui lòng xác định ảnh đại diện")]
        public bool is_avatar { get; set; }

        public DateTime create_at { get; set; }
    }
}
