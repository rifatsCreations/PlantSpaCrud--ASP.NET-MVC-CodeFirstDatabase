using PlantSpaCrud.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace PlantSpaCrud.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext() : base("AppDbContext")
        { }
        public virtual DbSet<Plant> Plants { get; set; }
        public virtual DbSet<Category>Categories  { get; set; }
        public virtual DbSet<PlantCareType> PlantCareTypes { get; set; }  
    }

    
}