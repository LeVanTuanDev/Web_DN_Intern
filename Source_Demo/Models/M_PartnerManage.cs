using System;
using System.ComponentModel.DataAnnotations;

namespace Source_Demo.Models
{
    public class M_PartnerManage : M_BaseModel.BaseCustom
    {
        public int partner_id { get; set; }
        public string partner_name { get; set; }
        public string abbreviation_name { get; set; }
        public string logo_image { get; set; }
        public string website { get; set; }
        public string contact_email { get; set; }
        public string phone { get; set; }
        public string note { get; set; }
        public int category_id { get; set; }
        public DateTime created_at { get; set; }
    }

    public class EM_PartnerManage : M_BaseModel.BaseCustom
    {
        [Required(ErrorMessage = "Vui lòng nhập mã đối tác")]
        public int partner_id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên đối tác")]
        [StringLength(200, ErrorMessage = "Tên đối tác tối đa 200 ký tự")]
        public string partner_name { get; set; }

        [StringLength(100, ErrorMessage = "Tên viết tắt tối đa 100 ký tự")]
        public string abbreviation_name { get; set; }

        [StringLength(250, ErrorMessage = "Đường dẫn hình/logo tối đa 250 ký tự")]
        public string logo_image { get; set; }

        [StringLength(250, ErrorMessage = "Website tối đa 250 ký tự")]
        public string website { get; set; }

        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [StringLength(200, ErrorMessage = "Email tối đa 200 ký tự")]
        public string contact_email { get; set; }

        [StringLength(50, ErrorMessage = "Số điện thoại tối đa 50 ký tự")]
        public string phone { get; set; }

        public string note { get; set; }

        public int category_id { get; set; }

        public DateTime created_at { get; set; }
    }
}
