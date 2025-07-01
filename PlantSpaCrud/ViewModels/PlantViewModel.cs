using PlantSpaCrud.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlantSpaCrud.ViewModels
{
    public class PlantViewModel
    {
        public int PlantId { get; set; }
        [Required(ErrorMessage = "Plant Name is required.")]
        [Display(Name = "Plant Name")]
        public string PlantName { get; set; }
        [Display(Name = "Is Flower Bearing?")]
        public bool FlowerBearing { get; set; }
        public decimal PlantPrice { get; set; }
        [Required(ErrorMessage = "Imported Date is required.")]
        [Display(Name = "Imported Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public System.DateTime Date { get; set; }
        [Display(Name = "Image")]
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "Category is required.")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int PlantCareTypeId { get; set; }
        public string InsecticideName { get; set; }
        public string Fertilizer { get; set; }
        [Display(Name = "Upload Picture")]
        public HttpPostedFileBase ProfileFile { get; set; }

        public virtual IList<Category> Categories { get; set; }
        public virtual IList<Plant>Plants  { get; set; }
        public virtual IList<PlantCareType> PlantCareTypes { get; set; } = new List<PlantCareType>();
    }
}