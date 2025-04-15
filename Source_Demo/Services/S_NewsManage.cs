using Source_Demo.Lib;
using Source_Demo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Source_Demo.Services
{
    public interface IS_News
    {
        Task<ResponseData<List<M_NewsManage>>> getListNewsByStatus(int? status);
        Task<ResponseData<M_NewsManage>> getNews(int id);
        Task<ResponseData<M_NewsManage>> Create(EM_NewsManage model, int createdBy);
        Task<ResponseData<M_NewsManage>> Update(EM_NewsManage model);
        Task<ResponseData<M_NewsManage>> Delete(int id);
        Task<ResponseData<M_NewsManage>> UpdateStatus(int id, int status);
        Task<ResponseData<M_NewsManage>> AddAvatarPost(int news_id, string avatarUrl, int updatedBy);
    }

    public class S_News : IS_News
    {
        private readonly ICallApi _callApi;
        public S_News(ICallApi callApi)
        {
            _callApi = callApi;
        }

        public async Task<ResponseData<List<M_NewsManage>>> getListNewsByStatus(int? status)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                { "status", status }
            };
            return await _callApi.GetResponseDataAsync<List<M_NewsManage>>(GlobalVariables.url_api + "News/GetListByStatus", dictPars);
        }

        public async Task<ResponseData<M_NewsManage>> getNews(int id)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                { "news_id", id }
            };
            return await _callApi.GetResponseDataAsync<M_NewsManage>(GlobalVariables.url_api + "News/GetById", dictPars);
        }

        public async Task<ResponseData<M_NewsManage>> Create(EM_NewsManage model, int createdBy)
        {
            model = CleanXSSHelper.CleanXSSObject(model);

            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                { "news_id", model.news_id },
                { "news_category_id", model.news_category_id },
                { "news_name", model.news_name },
                { "short_description", model.short_description },
                { "content", model.content },
                { "status_", true },              
                { "name_slug", model.name_slug },
                { "createdBy", createdBy }
            };
            return await _callApi.PostResponseDataAsync<M_NewsManage>(GlobalVariables.url_api + "News/Create", dictPars);
        }   

        public async Task<ResponseData<M_NewsManage>> Update(EM_NewsManage model)
        {
            model = CleanXSSHelper.CleanXSSObject(model);
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                { "news_id", model.news_id },
                { "news_category_id", model.news_category_id },
                { "news_name", model.news_name },
                { "short_description", model.short_description },
                { "content", model.content },
                { "status_", model.status_ },
                { "name_slug", model.name_slug }
            };
            return await _callApi.PutResponseDataAsync<M_NewsManage>(GlobalVariables.url_api + "News/Update", dictPars);
        }

        public async Task<ResponseData<M_NewsManage>> Delete(int id)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                { "news_id", id }
            };
            return await _callApi.DeleteResponseDataAsync<M_NewsManage>(GlobalVariables.url_api + "News/Delete", dictPars);
        }

        public async Task<ResponseData<M_NewsManage>> UpdateStatus(int id, int status)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                { "news_id", id },
                { "status_", status }
            };
            return await _callApi.PutResponseDataAsync<M_NewsManage>(GlobalVariables.url_api + "News/UpdateStatus", dictPars);
        }

        public async Task<ResponseData<M_NewsManage>> AddAvatarPost(int news_id, string avatarUrl, int updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                { "news_id", news_id },
                { "avatarUrl", avatarUrl },
                { "updatedBy", updatedBy }
            };
            return await _callApi.PutResponseDataAsync<M_NewsManage>(GlobalVariables.url_api + "News/AddAvatarPost", dictPars);
        }
    }
}
