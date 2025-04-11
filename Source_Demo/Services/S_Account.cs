using Source_Demo.Lib;
using Source_Demo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Source_Demo.Services
{
    public interface IS_Account
    {
        Task<ResponseData<M_Account>> Login(EM_LoginAccount model);
        Task<ResponseData<M_Account>> Register(EM_Account model);
        Task<ResponseData<M_Account>> Update(EM_Account model, int updatedBy);
        Task<ResponseData<M_Account>> Delete(EM_Account model);
        Task<ResponseData<M_Account>> UpdateAvatarURL(EM_Account model, int updatedBy);
    }

    public class S_Account : IS_Account
    {
        private readonly ICallApi _callApi;
        public S_Account(ICallApi callApi)
        {
            _callApi = callApi;
        }

        public async Task<ResponseData<M_Account>> Login(EM_LoginAccount model)
        {
            // Lọc XSS nếu cần
            model = CleanXSSHelper.CleanXSSObject(model);
            // Sử dụng đúng tên thuộc tính theo model mới: username và pass_word
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"Username", model.username},
                {"PassWord", model.pass_word}
            };
            return await _callApi.PostResponseDataAsync<M_Account>(GlobalVariables.url_api + "Account/Login", dictPars);
        }

        public async Task<ResponseData<M_Account>> Register(EM_Account model)
        {
            model = CleanXSSHelper.CleanXSSObject(model);
            // Sử dụng các trường có trong model mới: username, pass_word, avatar_url, employee_id, role_id
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"Username", model.username},
                {"PassWord", model.pass_word},
                {"AvatarUrl", model.avatar_url},
                {"EmployeeId", model.employee_id},
                {"RoleId", model.role_id}
            };
            return await _callApi.PostResponseDataAsync<M_Account>(GlobalVariables.url_api + "Account/Create", dictPars);
        }

        public async Task<ResponseData<M_Account>> Update(EM_Account model, int updatedBy)
        {
            model = CleanXSSHelper.CleanXSSObject(model);
            // Cập nhật các trường tương ứng với model mới
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"Username", model.username},
                {"PassWord", model.pass_word},
                {"AvatarUrl", model.avatar_url},
                {"EmployeeId", model.employee_id},
                {"RoleId", model.role_id},
                {"updatedBy", updatedBy}
            };
            return await _callApi.PutResponseDataAsync<M_Account>(GlobalVariables.url_api + "Account/Update", dictPars);
        }

        public async Task<ResponseData<M_Account>> Delete(EM_Account model)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"Username", model.username}
            };
            return await _callApi.DeleteResponseDataAsync<M_Account>(GlobalVariables.url_api + "Account/Delete", dictPars);
        }

        public async Task<ResponseData<M_Account>> UpdateAvatarURL(EM_Account model, int updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"Username", model.username},
                {"AvatarUrl", model.avatar_url},
                {"updatedBy", 8386}
            };
            return await _callApi.PutResponseDataAsync<M_Account>(GlobalVariables.url_api + "Account/ChangeAvatar", dictPars);
        }
    }
}