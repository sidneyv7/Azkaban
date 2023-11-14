using Microsoft.EntityFrameworkCore;

namespace Azkaban.Models
{
  public class ApplicationDbContext: DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    { }
                    public DbSet<Villa> Villas { get; set; }
    public DbSet<VillaNumber> VillaNumbers { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      //    base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<Villa>().HasData(
          new Villa
          {
            Id = 1,
            Name = "Royal Villa",
            Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
            ImageUrl = "~/images/VillaImage/a78075cc-2905-4471-a578-1d5fe3945fcd.jpg",
            Occupancy = 4,
            Price = 200,
            Sqft = 550,
          },
              new Villa
              {
                Id = 2,
                Name = "Premium Pool Villa",
                Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                ImageUrl = "~/images/VillaImage/cac7e1b4-5ba4-493f-9b73-14434d5b56d7.jpg",
                Occupancy = 4,
                Price = 300,
                Sqft = 550,
              },
              new Villa
              {
                Id = 3,
                Name = "Luxury Pool Villa",
                Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                ImageUrl = "~/images/VillaImage/d75cb0ad-9ed1-46af-87bc-fa99e00b2e58.jpg",
                Occupancy = 4,
                Price = 400,
                Sqft = 750,
              });

      modelBuilder.Entity<VillaNumber>().HasData(
         new VillaNumber
         {
           Villa_Number = 101,
           VillaId = 1,
         },
         new VillaNumber
         {
           Villa_Number = 102,
           VillaId = 1,
         },
         new VillaNumber
         {
           Villa_Number = 103,
           VillaId = 1,
         },
         new VillaNumber
         {
           Villa_Number = 104,
           VillaId = 1,
         },
         new VillaNumber
         {
           Villa_Number = 201,
           VillaId = 2,
         },
         new VillaNumber
         {
           Villa_Number = 202,
           VillaId = 2,
         },
         new VillaNumber
         {
           Villa_Number = 203,
           VillaId = 2,
         },
         new VillaNumber
         {
           Villa_Number = 301,
           VillaId = 3,
         },
         new VillaNumber
         {
           Villa_Number = 302,
           VillaId = 3,
         }
         );
    }
  }
}

