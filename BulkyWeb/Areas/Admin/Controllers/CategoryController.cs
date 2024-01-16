using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Admin.Controllers;

[Area("Admin")]
public class CategoryController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    
    public CategoryController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        return View(_unitOfWork.CategoryRepository.GetAll().ToList());
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Category category)
    {
        /*
        if (category.Name == category.DisplayOrder.ToString())
        {
            ModelState.AddModelError("name", "The Display Order cannot exactly match the Name.");
        }
        */

        if (!ModelState.IsValid)
        {
            return View();
        }
        
        _unitOfWork.CategoryRepository.Add(category);
        _unitOfWork.Save();
        
        TempData["success"] = "Category created successfully";
        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if (id is null or 0)
        {
            return NotFound();
        }

        var category = _unitOfWork.CategoryRepository.Get(u => u.Id == id);
        if (category == null)
        {
            return NotFound();
        }
        
        return View(category);
    }

    [HttpPost]
    public IActionResult Edit(Category category)
    {
        if (!ModelState.IsValid)
        {
            return View(category);
        }
        
        _unitOfWork.CategoryRepository.Update(category);
        _unitOfWork.Save();
        
        TempData["success"] = "Category updated successfully";
        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public IActionResult Delete(int? id)
    {
        if (id is null or 0)
        {
            return NotFound();
        }

        var category = _unitOfWork.CategoryRepository.Get(u => u.Id == id);
        if (category == null)
        {
            return NotFound();
        }
        
        return View(category);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        var category = _unitOfWork.CategoryRepository.Get(u => u.Id == id);
        if (category == null)
        {
            return NotFound();
        }
        
        _unitOfWork.CategoryRepository.Remove(category);
        _unitOfWork.Save();
        
        TempData["success"] = "Category deleted successfully";
        return RedirectToAction("Index");
    }
}
