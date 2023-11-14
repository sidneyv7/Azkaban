using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Azkaban.Models.ViewMoldes
{
  public class AmenityVM
  {
    public Amenity? Amenity { get; set; }
    [ValidateNever]
    public IEnumerable<SelectListItem>? VillaList { get; set; }
  }
}
