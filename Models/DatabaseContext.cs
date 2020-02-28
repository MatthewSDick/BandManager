using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BandManager.Models
{
  public partial class DatabaseContext : DbContext
  {

    public DbSet<Band> Bands { get; set; }
    public DbSet<Album> Albums { get; set; }
    public DbSet<Song> Songs { get; set; }

    public void SignABand(Band newBand)
    {
      Bands.Add(newBand);
      SaveChanges();
    }

    public void ProduceAlbum(int bandID, Album newAlbum)
    {
      var bandToUpdate = Bands.First(b => b.Id == bandID);
      bandToUpdate.Albums.Add(newAlbum);
      SaveChanges();
    }

    public void DeactivateBand()
    {
      var signedBands = Bands.Where(b => b.IsSigned == true);
      Console.WriteLine("Please enter the number of the band you wish to deactivate.");
      foreach (var band in signedBands)
      {
        Console.WriteLine($" * {band.Id} - {band.Name}");
      }
      Console.WriteLine("");
      var bandToDeactivate = int.Parse(Console.ReadLine());
      var deactivateBand = Bands.First(b => b.Id == bandToDeactivate);
      deactivateBand.IsSigned = false;
      SaveChanges();
    }

    // public void ReactivateBand(int bandToActivate)
    // {
    //   var signedBands = Bands.Where(b => b.IsSigned == false);
    //   Console.WriteLine("The bands that are not signed are...");
    //   foreach (var band in signedBands)
    //   {
    //     Console.WriteLine($" * {band.Name}");
    //   }
    //   var activateBand = Bands.First(b => b.Id == bandToActivate);
    //   activateBand.IsSigned = true;
    //   SaveChanges();
    // }

    public void ReactivateBand()
    {
      var NotSignedBands = Bands.Where(b => b.IsSigned == false);
      Console.WriteLine("Please enter the number of the band you wish to reactivate.");
      foreach (var band in NotSignedBands)
      {
        Console.WriteLine($" * {band.Id} - {band.Name}");
      }
      Console.WriteLine("");
      var bandToActivate = int.Parse(Console.ReadLine());
      var activateBand = Bands.First(b => b.Id == bandToActivate);
      activateBand.IsSigned = true;
      SaveChanges();
    }

    public void ViewBandAlbums(int bandToListAlbums)
    {
      var bandToDisplay = Albums.Where(b => b.BandId == bandToListAlbums).ToList();

      foreach (var album in bandToDisplay)
      {
        Console.WriteLine($"{album.Title}");
      }
      Console.WriteLine("");
      Console.WriteLine("Hit ENTER to continue.");
      Console.ReadLine();
    }

    public void ViewAlbumsByRelease()
    {
      var allAlbumsByRelease = Albums.OrderBy(a => a.ReleaseDate);
      Console.WriteLine("Here is a list of albums by release date...");
      foreach (var album in allAlbumsByRelease)
      {
        Console.WriteLine($" * {album.ReleaseDate} - {album.Title}");
      }
      Console.WriteLine("");
      Console.WriteLine("Hit ENTER to continue.");
      Console.ReadLine();
    }

    public void ViewSongs()
    {
      Console.WriteLine("Here is a list of all the songs...");
      foreach (var song in Songs)
      {
        Console.Write($" * {song.Title}\n");
      }
      Console.WriteLine("");
      Console.WriteLine("Hit ENTER to continue.");
      Console.WriteLine("");
      Console.ReadLine();
    }
    public void ViewSignedBands()
    {
      var signedBands = Bands.Where(b => b.IsSigned == true);
      Console.WriteLine("The signed bands are...");
      foreach (var band in signedBands)
      {
        Console.WriteLine($" * {band.Name}");
      }
      Console.WriteLine("");
      Console.WriteLine("Hit ENTER to continue.");
      Console.ReadLine();
    }
    public void ViewNotSignedBands()
    {
      var signedBands = Bands.Where(b => b.IsSigned == false);
      Console.WriteLine("The bands that are not signed are...");
      foreach (var band in signedBands)
      {
        Console.WriteLine($" * {band.Name}");
      }
      Console.WriteLine("");
      Console.WriteLine("Hit ENTER to continue.");
      Console.ReadLine();
    }
    public void ShowAllBands()
    {
      Console.WriteLine("Here is a list of all the bands...");
      foreach (var band in Bands)
      {
        Console.Write($"{band.Id} - {band.Name}\n");
      }
      Console.WriteLine("");
    }









    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        optionsBuilder.UseNpgsql("server=localhost;database=BandManager");
      }
    }
  }
}
