using Microsoft.AspNetCore.Mvc;
using QuanLyCovid.Models;
using System.Security.Permissions;

namespace QuanLyCovid.Controllers
{
    public class DiemCachLyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult InsertDCL(DiemCachLy dcl)
        {
            int count;
            StoreContext context = new StoreContext("server=127.0.0.1;user id=root;password=;port=3306;database=qlcovid;");
            count = context.InsertDCL(dcl);

            if (count > 0)
                ViewData["thongbao"] = "Insert thành công";
            else
                ViewData["thongbao"] = "Insert không thành công";
            return View();
        }
        public IActionResult ListDCL()
        {
            StoreContext context = new StoreContext("server=127.0.0.1;user id=root;password=;port=3306;database=qlcovid;");
            return View(context.GetTenDCL());
        }
        public IActionResult LietKeTheoTenDCL(string madcl)
        {
            StoreContext context = new StoreContext("server=127.0.0.1;user id=root;password=;port=3306;database=qlcovid;");
            return View(context.LietKeThongTinCNTheoTenDCL(madcl));
        }
    }
}
