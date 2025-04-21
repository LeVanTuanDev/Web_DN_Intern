using Newtonsoft.Json;
using Source_Demo.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Source_Demo.Lib;

namespace Source_Demo.Services
{
    public interface IS_NewsManage
    {
        Task<ResponseData<List<M_NewsManage>>> GetListByStatusAsync(int? status);
        Task<ResponseData<M_NewsManage>> GetByIdAsync(int news_id);
        Task<ResponseData<M_NewsManage>> CreateAsync(EM_NewsManage model, int createdBy);
        Task<ResponseData<M_NewsManage>> UpdateAsync(EM_NewsManage model, int updatedBy);
        Task<ResponseData<M_NewsManage>> DeleteAsync(int news_id);
        Task<ResponseData<M_NewsManage>> UpdateStatusAsync(int news_id, bool status_);
        Task<ResponseData<M_NewsManage>> AddAvatarPostAsync(int news_id, string avatarUrl, int updatedBy);
    }
    public class S_NewsManage : IS_NewsManage
    {
        private readonly ICallApi _callApi;
        public S_NewsManage(ICallApi callApi)
        {
            _callApi = callApi;
        }

        public async Task<ResponseData<List<M_NewsManage>>> GetListByStatusAsync(int? status)
        {
            var pars = new Dictionary<string, dynamic> { { "status", status } };
            return await _callApi.GetResponseDataAsync<List<M_NewsManage>>(GlobalVariables.url_api + "News/GetListByStatus", pars);
        }

        public async Task<ResponseData<M_NewsManage>> GetByIdAsync(int news_id)
        {
            var pars = new Dictionary<string, dynamic> { { "news_id", news_id } };
            return await _callApi.GetResponseDataAsync<M_NewsManage>(GlobalVariables.url_api + "News/GetById", pars);
        }

        public async Task<ResponseData<M_NewsManage>> CreateAsync(EM_NewsManage model, int createdBy)
        {
            model = CleanXSSHelper.CleanXSSObject(model);
            var pars = new Dictionary<string, dynamic>
            {
                { "news_category_id", model.news_category_id },
                { "news_name", model.news_name },
                { "short_description", model.short_description },
                { "content", model.content },
                { "status_", model.status_ },
                { "name_slug", model.name_slug },
                { "createdBy", createdBy }
            };
            return await _callApi.PostResponseDataAsync<M_NewsManage>(GlobalVariables.url_api + "News/Create", pars);
        }

        public async Task<ResponseData<M_NewsManage>> UpdateAsync(EM_NewsManage model, int updatedBy)
        {
            model = CleanXSSHelper.CleanXSSObject(model);
            var pars = new Dictionary<string, dynamic>
            {
                { "news_id", model.news_id },
                { "news_category_id", model.news_category_id },
                { "news_name", model.news_name },
                { "short_description", model.short_description },
                { "content", model.content },
                { "status_", model.status_ },
                { "name_slug", model.name_slug },
                { "updatedBy", updatedBy }
            };
            return await _callApi.PutResponseDataAsync<M_NewsManage>(GlobalVariables.url_api + "News/Update", pars);
        }

        public async Task<ResponseData<M_NewsManage>> DeleteAsync(int news_id)
        {
            var pars = new Dictionary<string, dynamic> { { "news_id", news_id } };
            return await _callApi.DeleteResponseDataAsync<M_NewsManage>(GlobalVariables.url_api + "News/Delete", pars);
        }

        public async Task<ResponseData<M_NewsManage>> UpdateStatusAsync(int news_id, bool status_)
        {
            var pars = new Dictionary<string, dynamic>
            {
                { "news_id", news_id },
                { "status_", status_ }
            };
            return await _callApi.PutResponseDataAsync<M_NewsManage>(GlobalVariables.url_api + "News/UpdateStatus", pars);
        }

        public async Task<ResponseData<M_NewsManage>> AddAvatarPostAsync(int news_id, string avatarUrl, int updatedBy)
        {
            var pars = new Dictionary<string, dynamic>
            {
                { "news_id", news_id },
                { "avatarUrl", avatarUrl },
                { "updatedBy", updatedBy }
            };
            return await _callApi.PutResponseDataAsync<M_NewsManage>(GlobalVariables.url_api + "News/AddAvatarPost", pars);
        }
    }
}
