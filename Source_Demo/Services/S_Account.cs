using Source_Demo.Lib;
using Source_Demo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Source_Demo.Services
{
    public interface IS_Account
    {
        Task<ResponseData<List<M_Account>>> getListAccountByStatus(int? status);
        Task<ResponseData<M_Account>> getAccount(int id);
        Task<ResponseData<M_Account>> Login(EM_LoginAccount model);
        Task<ResponseData<M_Account>> Register(EM_Account model);
        Task<ResponseData<M_Account>> Update(EM_Account model, int updatedBy);
        Task<ResponseData<M_Account>> Delete(int id);
        Task<ResponseData<M_Account>> UpdateStatus(int id, int status);
    }

    public class S_Account : IS_Account
    {
        private readonly ICallApi _callApi;
        public S_Account(ICallApi callApi)
        {
            _callApi = callApi;
        }

        public async Task<ResponseData<List<M_Account>>> getListAccountByStatus(int? status)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"status", status}
            };
            return await _callApi.GetResponseDataAsync<List<M_Account>>(GlobalVariables.url_api + "Account/GetListByStatus", dictPars);
        }

        public async Task<ResponseData<M_Account>> getAccount(int id)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id}
            };
            return await _callApi.GetResponseDataAsync<M_Account>(GlobalVariables.url_api + "Account/GetById", dictPars);
        }

        public async Task<ResponseData<M_Account>> Login(EM_LoginAccount model)
        {
            // Lọc XSS nếu cần
            model = CleanXSSHelper.CleanXSSObject(model);
            // Sử dụng đúng tên thuộc tính theo model mới: username và pass_word
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"username", model.username},
                {"pass_word", model.pass_word}
            };
            return await _callApi.PostResponseDataAsync<M_Account>(GlobalVariables.url_api + "Account/LoginOps", dictPars);
        }

        public async Task<ResponseData<M_Account>> Register(EM_Account model)
        {
            model = CleanXSSHelper.CleanXSSObject(model);
            // Sử dụng các trường có trong model mới: username, pass_word, avatar_url, employee_id, role_id
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"username", model.username},
                {"pass_word", model.pass_word},
                {"avatar_url", model.avatar_url},
                {"employee_id", model.employee_id},
                {"role_id", model.role_id}
            };
            return await _callApi.PostResponseDataAsync<M_Account>(GlobalVariables.url_api + "Account/Register", dictPars);
        }

        public async Task<ResponseData<M_Account>> Update(EM_Account model, int updatedBy)
        {
            model = CleanXSSHelper.CleanXSSObject(model);
            // Cập nhật các trường tương ứng với model mới
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"username", model.username},
                {"pass_word", model.pass_word},
                {"avatar_url", model.avatar_url},
                {"employee_id", model.employee_id},
                {"role_id", model.role_id},
                {"updatedBy", updatedBy}
            };
            return await _callApi.PutResponseDataAsync<M_Account>(GlobalVariables.url_api + "Account/Update", dictPars);
        }

        public async Task<ResponseData<M_Account>> Delete(int id)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id}
            };
            return await _callApi.DeleteResponseDataAsync<M_Account>(GlobalVariables.url_api + "Account/Delete", dictPars);
        }

        public async Task<ResponseData<M_Account>> UpdateStatus(int id, int status)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
                {"status", status},
                {"updatedBy", 8386} 
            };
            return await _callApi.PutResponseDataAsync<M_Account>(GlobalVariables.url_api + "Account/UpdateStatus", dictPars);
        }
    }
}
