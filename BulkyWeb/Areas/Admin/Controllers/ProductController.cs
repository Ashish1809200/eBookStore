using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Product> objProduct = _unitOfWork.Product.GetAll().ToList();
            return View(objProduct);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if(id==0 || id == null)
            {
                return NotFound();
            }
             Product? productObj = _unitOfWork.Product.Get(p => p.Id == id);
             return View(productObj);            
        }

        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null) return NotFound();
            Product? productObj = _unitOfWork.Product.Get(p => p.Id == id);
            return View(productObj);
        }

        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            if (id == 0 || id == null) return NotFound();
            Product? productObj = _unitOfWork.Product.Get(p => p.Id == id);
            if(productObj == null) return NotFound();
            _unitOfWork.Product.Remove(productObj);
            _unitOfWork.Save();
            TempData["success"] = "Product deleted successfully";
            return RedirectToAction("Index");

        }
}
