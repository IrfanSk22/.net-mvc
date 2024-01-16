using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    
    public ProductController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        return View(_unitOfWork.ProductRepository.GetAll().ToList());
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Product product)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        
        _unitOfWork.ProductRepository.Add(product);
        _unitOfWork.Save();
        
        TempData["success"] = "Product created successfully";
        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if (id is null or 0)
        {
            return NotFound();
        }

        var product = _unitOfWork.ProductRepository.Get(u => u.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        
        return View(product);
    }

    [HttpPost]
    public IActionResult Edit(Product product)
    {
        if (!ModelState.IsValid)
        {
            return View(product);
        }
        
        _unitOfWork.ProductRepository.Update(product);
        _unitOfWork.Save();
        
        TempData["success"] = "Product updated successfully";
        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public IActionResult Delete(int? id)
    {
        if (id is null or 0)
        {
            return NotFound();
        }

        var product = _unitOfWork.ProductRepository.Get(u => u.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        
        return View(product);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        var product = _unitOfWork.ProductRepository.Get(u => u.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        
        _unitOfWork.ProductRepository.Remove(product);
        _unitOfWork.Save();
        
        TempData["success"] = "Product deleted successfully";
        return RedirectToAction("Index");
    }
}
