using crud.Data;
using crud.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;

namespace crud.Controllers
{
    public class HomeController : Controller
    {
       
        private readonly IDbConnection _IDbConnection;

        public HomeController(IDbConnection IDbConnection)
        {
            _IDbConnection = IDbConnection;
        }

        public IActionResult Index()
        {
            var product = _IDbConnection.Query<Product>("GetProducts",null,commandType:CommandType.StoredProcedure);

            return View(product);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
