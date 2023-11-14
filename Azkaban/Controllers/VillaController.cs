using Azkaban.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Azkaban.Controllers
{
  public class VillaController : Controller
  {
    private readonly ApplicationDbContext _db;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public VillaController(ApplicationDbContext db,IWebHostEnvironment webHostEnvironment)
    {
      _db = db;
_webHostEnvironment = webHostEnvironment;
    }

    public IActionResult Index()
    {
      var villas = _db.Villas.ToList();
      return View(villas);
    }
    public IActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public IActionResult Create(Villa obj)
    {
      if (obj.Name == obj.Description)
      {
        ModelState.AddModelError("name", "The description cannot exactly match the Name.");
      }

      if (ModelState.IsValid)
      {
        if (obj.Image != null)
        {
          string fileName = Guid.NewGuid().ToString() + Path.GetExtension(obj.Image.FileName);
          string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, @"images\VillaImage");

          using var fileStream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create);
          obj.Image.CopyTo(fileStream);

          obj.ImageUrl = @"\images\VillaImage\" + fileName;
        }
        else
        {
          obj.ImageUrl = "https://placehold.co/600x400";
        }


        _db.Villas.Add(obj);
        _db.SaveChanges();
        TempData["success"] = "The villa has been created successfully.";

        return RedirectToAction("Index");
      }
      return View();
    }

    public IActionResult Update(int villaId)
    {
      Villa? obj = _db.Villas.FirstOrDefault(u => u.Id == villaId);
      //Villa? obj = _db.Villas.Find(villaId);
      //var VillaList = _db.Villas.Where(u => u.Price > 50 && u.Occupancy > 0);

      if (obj == null)
      {
        return RedirectToAction("Error", "Home");
      }
      return View(obj);
    }
    [HttpPost]
    public IActionResult Update(Villa obj)
    {
      if (ModelState.IsValid && obj.Id > 0)
      {
        if (obj.Image != null)
        {
          string fileName = Guid.NewGuid().ToString() + Path.GetExtension(obj.Image.FileName);
          string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, @"images\VillaImage");

          if (!string.IsNullOrEmpty(obj.ImageUrl))
          {
            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
              System.IO.File.Delete(oldImagePath);
            }
          }

          using var fileStream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create);
          obj.Image.CopyTo(fileStream);

          obj.ImageUrl = @"\images\VillaImage\" + fileName;
        }

        _db.Villas.Update(obj);
        _db.SaveChanges();
        TempData["success"] = "The villa has been updated successfully.";

        return RedirectToAction("Index");
      }
      return View();
    }
    public IActionResult Delete(int villaId)
    {
      Villa? obj = _db.Villas.FirstOrDefault(u => u.Id == villaId);
      if (obj is null)
      {
        return RedirectToAction("Error", "Home");
      }
      return View(obj);
    }


    [HttpPost]
    public IActionResult Delete(Villa obj)
    {
      Villa? objFromDb = _db.Villas.FirstOrDefault(u => u.Id == obj.Id);
      if (objFromDb is not null)
      {
        if (!string.IsNullOrEmpty(objFromDb.ImageUrl))
        {
          var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, objFromDb.ImageUrl.TrimStart('\\'));

          if (System.IO.File.Exists(oldImagePath))
          {
            System.IO.File.Delete(oldImagePath);
          }
        }
        _db.Villas.Remove(objFromDb);
        _db.SaveChanges();
        TempData["success"] = "The villa has been deleted successfully.";
        return RedirectToAction("Index");
      }
      return View();
    }




  }

}
