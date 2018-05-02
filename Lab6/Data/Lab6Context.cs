using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Lab6.Models;


namespace Lab6.Models
{
    public class Lab6Context : DbContext
    {
        public Lab6Context(DbContextOptions<Lab6Context> options)
            : base(options)
        {
        }

        public DbSet<MvcMovie.Models.Movie> Movie { get; set; }

        public DbSet<Lab6.Models.Reviews> Reviews { get; set; }
    }
}
