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

    public void DeactivateBand(int bandToRelease)
    {
      var releaseBand = Bands.First(b => b.Id == bandToRelease);
      releaseBand.IsSigned = false;
      SaveChanges();
    }

    public void ReactivateBand(int bandToActivate)
    {
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
      //  View all albums for a band
    }

    public void ViewAlbumsByRelease()
    {
      var allAlbumsByRelease = Albums.OrderBy(a => a.ReleaseDate);
      foreach (var album in allAlbumsByRelease)
      {
        Console.WriteLine($"{album.Title}");

      }
    }

    public void ViewSongs()
    {
      //  View and Albums song
      var allSongs = Songs.OrderBy(s => s.Title);
      foreach (var song in allSongs)
      {
        Console.Write($"{song.Title}");
      }
    }

    public void ViewSignedBands()
    {
      var signedBands = Bands.Where(b => b.IsSigned == true);
      foreach (var band in signedBands)
      {
        Console.WriteLine($"{band.Name}");
      }
    }

    public void ViewNotSignedBands()
    {
      var signedBands = Bands.Where(b => b.IsSigned == false);
      foreach (var band in signedBands)
      {
        Console.WriteLine($"{band.Name}");
      }
    }
    public void ShowAllBands()
    {
      foreach (var band in Bands)
      {
        Console.Write($"{band.Id} - {band.Name}");
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
