using crud.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [HttpGet]
        public IActionResult Index()
        {
            var products = _IDbConnection.Query<Product>("sp_GetAllProducts", commandType: CommandType.StoredProcedure);
            return View(products);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            var product = _IDbConnection.QueryFirstOrDefault<Product>("sp_GetProductsById", parameters, commandType: CommandType.StoredProcedure);
            if (product == null)
            {
                return NotFound(); // Return 404 Not Found if product not found
            }

            return View(product);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Name", product.Name);
            parameters.Add("@Description", product.Description);
            parameters.Add("@CreatedAt", product.CreatedAt);
            parameters.Add("@Price", product.Price);
            _IDbConnection.Execute("sp_CreateProducts", parameters, commandType: CommandType.StoredProcedure);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            var product = _IDbConnection.QueryFirstOrDefault<Product>("sp_GetProductsById", parameters, commandType: CommandType.StoredProcedure);
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@id", product.Id, DbType.Int32);
            parameters.Add("@Name", product.Name, DbType.String);
            parameters.Add("@Description", product.Description, DbType.String);
            parameters.Add("@Price", product.Price, DbType.Int32);
            _IDbConnection.Execute("sp_UpdateProducts", parameters, commandType: CommandType.StoredProcedure);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            _IDbConnection.Execute("sp_DeleteProduct", parameters, commandType: CommandType.StoredProcedure);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            var product = _IDbConnection.QueryFirstOrDefault<Product>("sp_GetProductsById", parameters, commandType: CommandType.StoredProcedure);
            if (product == null)
            {
                return NotFound(); // Return 404 Not Found if product not found
            }
            return View(product);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
