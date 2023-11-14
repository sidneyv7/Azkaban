using Azkaban.Models;
using Azkaban.Models.ViewMoldes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Azkaban.Controllers
{
  public class AmenityController : Controller
  {
    private readonly ApplicationDbContext _db;
    public AmenityController(ApplicationDbContext context)
    {
      _db = context;

    }
    public IActionResult Index()
    {
      List<Amenity> amenities = _db.Amenities .Include(m => m.Villa).ToList();
      return View(amenities);
    }


    public IActionResult Create()
    {
      AmenityVM amenityVM = new()
      {
        VillaList = _db.Villas.ToList().Select(u => new SelectListItem
        {
          Text = u.Name,
          Value = u.Id.ToString()
        })
      };
      return View(amenityVM);
    }

    [HttpPost]
    public IActionResult Create(AmenityVM obj)
    {
      ModelState.Remove("Villa");
      if (ModelState.IsValid)
      {
        _db.Amenities.Add(obj.Amenity);
        _db.SaveChanges();
        TempData["success"] = "The amenity has been created successfully.";
        return RedirectToAction(nameof(Index));
      }

      obj.VillaList = _db.Villas.ToList().Select(u => new SelectListItem
      {
        Text = u.Name,
        Value = u.Id.ToString()
      });
      return View(obj);
    }


    public IActionResult Update(int amenityId)
    {
      AmenityVM amenityVM = new()
      {

        VillaList = _db.Villas.ToList().Select(u => new SelectListItem
        {
          Text = u.Name,
          Value = u.Id.ToString()
        }),
        Amenity = _db.Amenities.FirstOrDefault(u => u.Id == amenityId)
      };
      if (amenityVM.Amenity == null)
      {
        return RedirectToAction("Error", "Home");
      }
      return View(amenityVM);
    }

    [HttpPost]
    public IActionResult Update(AmenityVM amenityVM)
    {
      if (ModelState.IsValid)
      
        {
          _db.Amenities.Update(amenityVM.Amenity);
          _db.SaveChanges();
          TempData["success"] = "The villa Number has been updated successfully.";
          return RedirectToAction("Index");

        }
        amenityVM.VillaList = _db.Villas.ToList().Select(u => new SelectListItem
        {
          Text = u.Name,
          Value = u.Id.ToString()
        });
        return View(amenityVM);
      }


    public IActionResult Delete(int amenityId)
    {
      AmenityVM amenityVM = new()
      {

        VillaList = _db.Villas.ToList().Select(u => new SelectListItem
        {
          Text = u.Name,
          Value = u.Id.ToString()
        }),
        Amenity = _db.Amenities.FirstOrDefault(u => u.Id == amenityId)
      };
      if (amenityVM.Amenity == null)
      {
        return RedirectToAction("Error", "Home");
      }
      return View(amenityVM);

    }

    [HttpPost]
    public IActionResult Delete(AmenityVM amenityVM)
    {
      Amenity? objFromDb = _db.Amenities.FirstOrDefault(u => u.Id == amenityVM.Amenity.Id);

      if (objFromDb is not null)
      {
        _db.Amenities.Remove(objFromDb);
        _db.SaveChanges();
        TempData["success"] = "The amenity has been deleted successfully.";
        return RedirectToAction("Index");

      }
      TempData["error"] = "The amenity could not be deleted.";
      return View();

    }


    }
  }


