using System;
using BandManager.Models;
using System.Linq;
using ConsoleTools;

namespace BandManager
{
  class Program
  {


    static int verifyNumber(string toCheck)
    {
      int returnNumber;
      while (!Int32.TryParse(toCheck, out returnNumber))
      {
        Console.WriteLine("Please enter a number.");
        toCheck = Console.ReadLine().ToLower();
      }
      return returnNumber;
    }

    static void AddBand()
    {
      var bandToAdd = new Band();
      Console.WriteLine($"What is the name of the band.");
      bandToAdd.Name = (Console.ReadLine());
      Console.WriteLine($"What country is the band from?  ");
      bandToAdd.CountryOfOrigin = (Console.ReadLine());
      Console.WriteLine($"How many members are in the band?");
      var input = Console.ReadLine().ToLower();
      bandToAdd.NumberOfMembers = verifyNumber(input);
      Console.WriteLine($"What is the bands web site URL?");
      bandToAdd.Website = (Console.ReadLine());
      Console.WriteLine($"What is the bands style of music?");
      bandToAdd.Style = (Console.ReadLine());
      Console.WriteLine($"Who is the bands contact person?");
      bandToAdd.PersonOfContact = (Console.ReadLine());
      Console.WriteLine($"What is the contact persons phone number?");
      bandToAdd.ContactPhoneNumber = (Console.ReadLine());
      var Db = new DatabaseContext();
      Db.SignABand(bandToAdd);
    }

    static void ProduceAlbum()
    {
      var Db = new DatabaseContext();
      Db.ShowAllBands();
      var band = int.Parse(Console.ReadLine());
      var albumToAdd = new Album();
      Console.WriteLine("What is the title of the album?");
      albumToAdd.Title = (Console.ReadLine());
      Console.WriteLine("Is the album explicit? (Y) or (N)");
      var isExplicit = Console.ReadLine().ToLower();
      if (isExplicit == "y")
      {
        albumToAdd.IsExplicit = true;
      }
      else
      {
        albumToAdd.IsExplicit = false;
      }
      Db.ProduceAlbum(band, albumToAdd);
    }

    static void DeactivateBand()
    {
      var Db = new DatabaseContext();
      Db.DeactivateBand();
    }

    static void ReactivateBand()
    {
      var Db = new DatabaseContext();
      Db.ReactivateBand();
    }

    static void ShowAllBands()
    {
      var Db = new DatabaseContext();
      Db.ShowAllBands();
      Console.WriteLine("Hit ENTER to continue.");
      Console.ReadLine();
    }

    static void ViewAlbumsByRelease()
    {
      var Db = new DatabaseContext();
      Db.ViewAlbumsByRelease();
    }

    static void ViewSongs()
    {
      var Db = new DatabaseContext();
      Db.ViewSongs();
    }

    static void ViewSignedBands()
    {
      var Db = new DatabaseContext();
      Db.ViewSignedBands();
    }

    static void ViewNotSignedBands()
    {
      var Db = new DatabaseContext();
      Db.ViewNotSignedBands();
    }

    static void Main(string[] args)
    {
      //var Db = new DatabaseContext();

      new ConsoleMenu()
        .Add("Add Band", () => AddBand())
        .Add("Produce Album", () => ProduceAlbum())
        .Add("Deactivate Band", () => DeactivateBand())
        .Add("Reactivate Band", () => ReactivateBand())
        .Add("Show all bands", () => ShowAllBands())
        .Add("View albums by release date.", () => ViewAlbumsByRelease())
        .Add("View all songs", () => ViewSongs())
        .Add("View all signed bands", () => ViewSignedBands())
        .Add("View all bands not signed", () => ViewNotSignedBands())
        .Add("Close", ConsoleMenu.Close)
        .Configure(config =>
        {
          config.Selector = "-->";
          config.Title = "*** Band Manager ***";
          config.EnableWriteTitle = true;
          config.ClearConsole = true;
        }
          )
        .Show();





    }
  }
}
