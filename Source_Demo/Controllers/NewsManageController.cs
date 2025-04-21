using Microsoft.AspNetCore.Mvc;
using Source_Demo.Lib;
using Source_Demo.Models;
using Source_Demo.Services;
using System.Threading.Tasks;

namespace Source_Demo.Controllers
{
    public class NewsManageController : BaseController<NewsManageController>
    {
        private readonly IS_NewsManage _s_News;

        public NewsManageController(IS_NewsManage sNews)
        {
            _s_News = sNews;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult P_Add()
        {
            return PartialView();
        }

        [HttpGet]
        public async Task<IActionResult> P_Edit(int id)
        {
            var result = await _s_News.GetByIdAsync(id);
            return PartialView(result.data);
        }

        [HttpGet]
        public async Task<JsonResult> GetListByStatus(int? status = 1)
        {
            var res = await _s_News.GetListByStatusAsync(status);
            var jResult = new M_JResult();
            return Json(jResult.MapData(res));
        }

        [HttpPost]
        public async Task<JsonResult> Create(EM_NewsManage model)
        {
            var res = await _s_News.CreateAsync(model, createdBy: 1); 
            var jResult = new M_JResult();
            return Json(jResult.MapData(res));
        }

        [HttpPost]
        public async Task<JsonResult> Update(EM_NewsManage model)
        {
            var res = await _s_News.UpdateAsync(model, updatedBy: 1); 
            var jResult = new M_JResult();
            return Json(jResult.MapData(res));
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int news_id)
        {
            var res = await _s_News.DeleteAsync(news_id);
            var jResult = new M_JResult();
            return Json(jResult.MapData(res));
        }

        [HttpPost]
        public async Task<JsonResult> UpdateStatus(int news_id, bool status_)
        {
            var res = await _s_News.UpdateStatusAsync(news_id, status_);
            var jResult = new M_JResult();
            return Json(jResult.MapData(res));
        }

        [HttpPost]
        public async Task<JsonResult> AddAvatarPost(int news_id, string avatar_url)
        {
            var res = await _s_News.AddAvatarPostAsync(news_id, avatar_url, updatedBy: 1);
            var jResult = new M_JResult();
            return Json(jResult.MapData(res));
        }
    }
}
