using System.ComponentModel.DataAnnotations;

namespace Source_Demo.Models
{
    public class M_FieldOfActivity : M_BaseModel.BaseCustom
    {
        public int field_id { get; set; }
        public string field_name { get; set; }
        public int status_ { get; set; }
    }

    public class EM_FieldOfActivity : M_BaseModel.BaseCustom
    {
        public int field_id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên lĩnh vực")]
        [StringLength(50, ErrorMessage = "Tên lĩnh vực có độ dài tối đa 50 ký tự")]
        public string field_name { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn trạng thái")]
        [Range(0, 1, ErrorMessage = "Trạng thái chỉ được chọn 0 hoặc 1")]
        public int status_ { get; set; }
    }
}
