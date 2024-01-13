using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Controllers;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _db;
    
    public CategoryController(ApplicationDbContext db)
    {
        _db = db;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View(await _db.Categories.ToListAsync());
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Category category)
    {
        /*
        if (category.Name == category.DisplayOrder.ToString())
        {
            ModelState.AddModelError("name", "The Display Order cannot exactly match the Name.");
        }
        */
        
        if (ModelState.IsValid)
        {
            _db.Categories.Add(category);
            await _db.SaveChangesAsync();
            TempData["success"] = "Category created successfully";
            return RedirectToAction("Index");
        }

        return View();
    }
    
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        var category = await _db.Categories.FindAsync(id);
        if (category == null)
        {
            return NotFound();
        }
        
        return View(category);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Category category)
    {
        if (ModelState.IsValid)
        {
            _db.Categories.Update(category);
            await _db.SaveChangesAsync();
            TempData["success"] = "Category updated successfully";
            return RedirectToAction("Index");
        }

        return View(category);
    }
    
    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        var category = await _db.Categories.FindAsync(id);
        if (category == null)
        {
            return NotFound();
        }
        
        return View(category);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var category = await _db.Categories.FindAsync(id);
        if (category == null)
        {
            return NotFound();
        }

        _db.Categories.Remove(category);
        await _db.SaveChangesAsync();
        TempData["success"] = "Category deleted successfully";
        return RedirectToAction("Index");
    }
}
