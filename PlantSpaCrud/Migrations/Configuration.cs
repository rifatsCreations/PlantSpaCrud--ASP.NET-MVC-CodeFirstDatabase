namespace PlantSpaCrud.Migrations
{
    using PlantSpaCrud.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PlantSpaCrud.DAL.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PlantSpaCrud.DAL.AppDbContext context)
        {
            var categoryList = new List<Category>
            {
                new Category{CategoryName="Indoor"},
                new Category{CategoryName="Outdoor"},
                new Category{CategoryName="SemiIndoor"}
            };
            categoryList.ForEach(p => context.Categories.AddOrUpdate(c => c.CategoryName, p));
            context.SaveChanges();
        }
    }
}
