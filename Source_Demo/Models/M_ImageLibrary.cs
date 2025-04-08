using System;

namespace Source_Demo.Models
{
    public class M_ImageLibrary : M_BaseModel.BaseCustom
    {
        public int id { get; set; }
        public int library_id { get; set; }
        public string video_title { get; set; }
        public string video_url { get; set; }
        public DateTime uploaded_at { get; set; }
        public string username { get; set; }
    }

    // Model mở rộng cho Validation
    public class EM_ImageLibrary : M_BaseModel.BaseCustom
    {
        public int id { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Vui lòng nhập Library ID")]
        public int library_id { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Vui lòng nhập tiêu đề video")]
        public string video_title { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Vui lòng nhập URL video")]
        public string video_url { get; set; }

        public DateTime uploaded_at { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Vui lòng nhập tài khoản người đăng")]
        public string username { get; set; }
    }
}
