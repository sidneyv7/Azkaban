using Azkaban.Models;
using Azkaban.Models.ViewMoldes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;

namespace Azkaban.Controllers
{
  public class VillaNumberController : Controller
  {
    private readonly ApplicationDbContext _context;

    public VillaNumberController(ApplicationDbContext context)
    {
      _context = context;
    }

    public IActionResult Index()
    {

      List<VillaNumber> villaNumbers = _context.VillaNumbers.Include(s => s.Villa).ToList();
      return View(villaNumbers);
    }

    public IActionResult Create()
    {
      VillaNumberVM villaNumberVM = new()
      {
        VillaList = _context.Villas.ToList().Select(u => new SelectListItem
        {
          Text = u.Name,
          Value = u.Id.ToString()
        })
      };
      return View(villaNumberVM);
    }

    [HttpPost]
    public IActionResult Create(VillaNumberVM obj)
    {
      //ModelState.Remove("Villa");

      bool roomNumberExists = _context.VillaNumbers.Any(u => u.Villa_Number == obj.VillaNumber.Villa_Number);

      if (ModelState.IsValid && !roomNumberExists)
      {
        _context.VillaNumbers.Add(obj.VillaNumber);
        _context.SaveChanges();
        TempData["success"] = "The villa Number has been created successfully.";
        return RedirectToAction("Index");
      }

      if (roomNumberExists)
      {
        TempData["error"] = "The villa Number already exists.";
      }
      obj.VillaList = _context.Villas.ToList().Select(u => new SelectListItem
      {
        Text = u.Name,
        Value = u.Id.ToString()
      });
      return View(obj);
    }


    public IActionResult Update(int villaNumberId)
    {
      VillaNumberVM villaNumberVM = new()
      {

        VillaList = _context.Villas.ToList().Select(u => new SelectListItem
        {
          Text = u.Name,
          Value = u.Id.ToString()
        }),
        VillaNumber = _context.VillaNumbers.FirstOrDefault(u => u.Villa_Number == villaNumberId)
      };
      if (villaNumberVM.VillaNumber == null)
      {
        return RedirectToAction("Error", "Home");
      }
      return View(villaNumberVM);
    }

    [HttpPost]
    public IActionResult Update(VillaNumberVM villaNumberVM)
    {

      if (ModelState.IsValid)
      {
        _context.VillaNumbers.Update(villaNumberVM.VillaNumber);
        _context.SaveChanges();
        TempData["success"] = "The villa Number has been updated successfully.";
        return RedirectToAction("Index");
      }

      villaNumberVM.VillaList = _context.Villas.ToList().Select(u => new SelectListItem
      {
        Text = u.Name,
        Value = u.Id.ToString()
      });
      return View(villaNumberVM);
    }


    public IActionResult Delete(int villaNumberId)
    {
      VillaNumberVM villaNumberVM = new()
      {
        VillaList = _context.Villas.ToList().Select(u => new SelectListItem
        {
          Text = u.Name,
          Value = u.Id.ToString()
        }),
        VillaNumber = _context.VillaNumbers.FirstOrDefault(u => u.Villa_Number == villaNumberId)
      };
      if (villaNumberVM.VillaNumber == null)
      {
        return RedirectToAction("Error", "Home");
      }
      return View(villaNumberVM);
    }



    [HttpPost]
    public IActionResult Delete(VillaNumberVM villaNumberVM)
    {


      VillaNumber? objFromDb = _context.VillaNumbers
          .FirstOrDefault(u => u.Villa_Number == villaNumberVM.VillaNumber.Villa_Number);
      if (objFromDb is not null)
      {
        _context.VillaNumbers.Remove(objFromDb);
        _context.SaveChanges();
        TempData["success"] = "The villa number has been deleted successfully.";
        return RedirectToAction("Index");
      }
      TempData["error"] = "The villa number could not be deleted.";
      return View();
    }
  }
}
