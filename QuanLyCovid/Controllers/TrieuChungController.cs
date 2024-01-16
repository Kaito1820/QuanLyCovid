using Microsoft.AspNetCore.Mvc;
using QuanLyCovid.Models;

namespace QuanLyCovid.Controllers
{
    public class TrieuChungController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult LietKe(int soluong)
        {

            StoreContext context = new StoreContext("server=127.0.0.1;user id=root;password=;port=3306;database=qlcovid;");
            //ViewData["lietke"] = context.GetSoLuong(soluong);
            return View(context.GetSoLuong(soluong));
        }
    }
}
