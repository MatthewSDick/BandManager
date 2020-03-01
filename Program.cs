using System;
using BandManager.Models;
using System.Linq;
using ConsoleTools;
using System.Collections.Generic;

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
      Console.Clear();
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
      Console.Clear();
      var Db = new DatabaseContext();
      Db.ShowAllBands();
      Console.WriteLine("Please enter the number of the band the new album is for");
      var band = int.Parse(Console.ReadLine());
      var albumToAdd = new Album();
      Console.Clear();
      Console.WriteLine("What is the title of the album?");
      albumToAdd.Title = (Console.ReadLine());
      Console.Clear();
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

      // Loop for list of songs
      var listOfSongs = new List<Song>();

      var addSongs = true;

      while (addSongs)
      {
        Console.WriteLine();
        Console.Clear();
        Console.WriteLine("What is the title of the song?");
        var songTitle = Console.ReadLine();
        Console.Clear();
        Console.WriteLine("What are the lyrics? for the song");
        var songLyrics = Console.ReadLine();
        Console.Clear();
        Console.WriteLine("What is the length of the song?");
        var songLength = Console.ReadLine();
        Console.Clear();
        Console.WriteLine("What is the genre of the song?");
        var songGenre = Console.ReadLine();
        var newSongToAdd = new Song
        {
          Title = songTitle,
          Lyrics = songLyrics,
          Length = songLength,
          Genre = songGenre,
        };
        listOfSongs.Add(newSongToAdd);
        Console.Clear();
        Console.WriteLine($"Your song has been added.");
        Console.WriteLine("\n");
        Console.WriteLine("Do you want to add another song? (Y) or (N)");
        var anotherSong = Console.ReadLine().ToLower();

        if (anotherSong == "n")
        {
          addSongs = false;
        }

      }
      Db.ProduceAlbum(band, albumToAdd, listOfSongs);
    }

    static void DeactivateBand()
    {
      Console.Clear();
      var Db = new DatabaseContext();
      Db.DeactivateBand();
    }

    static void ReactivateBand()
    {
      Console.Clear();
      var Db = new DatabaseContext();
      Db.ReactivateBand();
    }

    static void ViewBandAlbums()
    {
      Console.Clear();
      var Db = new DatabaseContext();
      Db.ViewBandAlbums();
    }

    static void ViewAlbumsByRelease()
    {
      Console.Clear();
      var Db = new DatabaseContext();
      Db.ViewAlbumsByRelease();
    }

    static void ViewSongs()
    {
      // User picks band then album to see the songs
      Console.Clear();
      var Db = new DatabaseContext();
      Db.ShowAllBands();
      Console.WriteLine("Please enter the number for the band.");
      var band = int.Parse(Console.ReadLine());
      Console.Clear();
      // need by
      Db.GetAlbumNumber(band);
      Console.WriteLine("Please enter the number for the album.");
      var album = int.Parse(Console.ReadLine());
      Db.ViewSongs(album);
    }

    static void ViewSignedBands()
    {
      Console.Clear();
      var Db = new DatabaseContext();
      Db.ViewSignedBands();
    }

    static void ViewNotSignedBands()
    {
      Console.Clear();
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
        .Add("Show all albums for a band", () => ViewBandAlbums())
        .Add("View albums by release date.", () => ViewAlbumsByRelease())
        .Add("View all songs from a album", () => ViewSongs())
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
