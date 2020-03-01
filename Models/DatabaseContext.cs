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

    public void ProduceAlbum(int bandID, Album newAlbum, List<Song> newSongs)
    {
      var bandToUpdate = Bands.First(b => b.Id == bandID);
      bandToUpdate.Albums.Add(newAlbum);
      // loop the songs and add to the album
      foreach (var song in newSongs)
      {
        newAlbum.Songs.Add(song);
      }
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

    public void ViewBandAlbums()
    {
      Console.WriteLine("Here are the bands to choose from.");
      foreach (var band in Bands)
      {
        Console.Write($"{band.Id} - {band.Name}\n");
      }
      Console.WriteLine("");
      Console.WriteLine("Please enter the number for the band you want to see the albums for.");
      var bandToListAlbums = int.Parse(Console.ReadLine());
      var bandToDisplay = Albums.Where(b => b.BandId == bandToListAlbums).ToList();
      Console.Clear();
      Console.WriteLine("Here are the albums.");
      foreach (var album in bandToDisplay)
      {
        Console.WriteLine($" * {album.Title}");
      }
      Console.WriteLine("");
      Console.WriteLine("Hit ENTER to continue.");
      Console.ReadLine();
    }

    public void GetAlbumNumber(int inNumber)
    {
      var albumNumber = Albums.Where(b => b.BandId == inNumber);
      //var albumNumber = Albums.OrderBy(b => b.BandId == inNumber);

      Console.WriteLine("Here are the albums.");
      foreach (var album in albumNumber)
      {
        Console.WriteLine($" * {album.Id} - {album.Title}");
      }
      Console.WriteLine("");
    }

    public void ViewAlbumsByRelease()
    {
      var allAlbumsByRelease = Albums.OrderBy(a => a.ReleaseDate);
      Console.Clear();
      Console.WriteLine("Here is a list of albums by release date...");
      foreach (var album in allAlbumsByRelease)
      {
        Console.WriteLine($" * {album.ReleaseDate} - {album.Title}");
      }
      Console.WriteLine("");
      Console.WriteLine("Hit ENTER to continue.");
      Console.ReadLine();
    }

    public void ViewSongs(int albumIn)
    {
      // This must let user select the Band and album to see the songs. Sel by Alb
      Console.Clear();
      Console.WriteLine("Here is a list of all the songs.");
      Console.WriteLine("");
      var listOfSongs = Songs.Where(s => s.AlbumId == albumIn).OrderBy(s => s.Title);


      foreach (var song in listOfSongs)
      {
        Console.WriteLine($"{song.Title}");
      }

      Console.WriteLine("");
      Console.WriteLine("Hit ENTER to continue.");
      Console.ReadLine();
      //var listOfSongs = Bands.Include(band => band.Albums).ThenInclude(album => album.Songs);
      // foreach (var band in Bands)
      // {
      //   Console.WriteLine($"{band.Name}{band.Albums}");
      // }
      // Console.WriteLine("");
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
      var notSignedBands = Bands.Where(b => b.IsSigned == false);
      Console.WriteLine("The bands that are not signed are...");

      foreach (var band in notSignedBands)
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
    }

    public void ShowAllAlbums()
    {
      Console.WriteLine("Here is a list of all the bands...");
      foreach (var album in Albums)
      {
        Console.Write($"{album.Id} - {album.Title}\n");
      }
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
