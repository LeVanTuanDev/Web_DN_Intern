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
            model = CleanXSSHelper.CleanXSSObject(model);
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"userName", model.userName},
                {"password", model.password}
            };
            return await _callApi.PostResponseDataAsync<M_Account>(GlobalVariables.url_api + "Account/LoginOps", dictPars);
        }

        public async Task<ResponseData<M_Account>> Register(EM_Account model)
        {
            model = CleanXSSHelper.CleanXSSObject(model);
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"userName", model.userName},
                {"password", model.password},
                {"firstName", model.firstName},
                {"lastName", model.lastName},
                {"email", model.email},
                {"phone", model.phone}
            };
            return await _callApi.PostResponseDataAsync<M_Account>(GlobalVariables.url_api + "Account/Register", dictPars);
        }

        public async Task<ResponseData<M_Account>> Update(EM_Account model, int updatedBy)
        {
            model = CleanXSSHelper.CleanXSSObject(model);
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", model.id}, // đảm bảo gửi id nếu cần thiết cho API update
                {"userName", model.userName},
                {"firstName", model.firstName},
                {"lastName", model.lastName},
                {"email", model.email},
                {"phone", model.phone}
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
                {"updatedBy", 8386} // hoặc sử dụng biến updatedBy nếu có từ phía client
            };
            return await _callApi.PutResponseDataAsync<M_Account>(GlobalVariables.url_api + "Account/UpdateStatus", dictPars);
        }
    }
}
