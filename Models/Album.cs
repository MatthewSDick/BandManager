using System;
using System.Collections.Generic;

namespace BandManager.Models
{
  public class Album
  {
    public int Id { get; set; }

    public string Title { get; set; }

    public bool IsExplicit { get; set; }

    public int BandId { get; set; }

    public DateTime ReleaseDate { get; set; } = DateTime.Now;

    public List<Song> Songs { get; set; } = new List<Song>();

  }
}