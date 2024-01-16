using Microsoft.AspNetCore.Mvc;
using QuanLyCovid.Models;

namespace QuanLyCovid.Controllers
{
    public class CongNhanController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult DeleteCN(string? macn)
        {
            StoreContext context = new StoreContext("server=127.0.0.1;user id=root;password=;port=3306;database=qlcovid;");

            int count = context.DeleteCN(macn);
            if (count > 0)
            {
                ViewData["thongbao"] = "Xóa thành công";
            }
            else
            {
                ViewData["thongbao"] = "Xóa không thành công";
            }
            return View();
        }
        public IActionResult ViewCN(string? macn)
        {
            StoreContext context = new StoreContext("server=127.0.0.1;user id=root;password=;port=3306;database=qlcovid;");
            return View(context.ViewCN(macn));
        }
    }
}
