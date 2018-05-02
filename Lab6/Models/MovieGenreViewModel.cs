using Microsoft.AspNetCore.Mvc.Rendering;
using MvcMovie.Models;
using Lab6.Models;
using System.Collections.Generic;

namespace Lab6.Models
{
    public class MovieGenreViewModel
    {
        public List<Movie> movies;
        public SelectList genres;
        public string movieGenre { get; set; }
    }
}
