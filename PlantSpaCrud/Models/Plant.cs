using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlantSpaCrud.Models
{
    public class Plant
    {
        public Plant()
        {
            this.PlantCareTypes = new HashSet<PlantCareType>();
        }

        public int PlantId { get; set; }
        public string PlantName { get; set; }
        public bool FlowerBearing { get; set; }
        public decimal PlantPrice { get; set; }
        public System.DateTime Date { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<PlantCareType> PlantCareTypes { get; set; }
    }

    public class PlantCareType
    {
        public int PlantCareTypeId { get; set; }
        public string InsecticideName { get; set; }
        public string Fertilizer { get; set; }
        public int PlantId { get; set; }
        public virtual Plant Plant { get; set; }
    }

    public class Category
    {
        public Category()
        {
            this.Plants = new HashSet<Plant>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<Plant> Plants { get; set; }
    }
}