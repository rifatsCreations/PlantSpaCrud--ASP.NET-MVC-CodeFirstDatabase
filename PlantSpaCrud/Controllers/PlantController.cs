using PlantSpaCrud.DAL;
using PlantSpaCrud.Models;
using PlantSpaCrud.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlantSpaCrud.Controllers
{
    public class PlantController : Controller
    {
        AppDbContext db = new AppDbContext();
        public ActionResult Index()
        {
            IEnumerable<Plant> plants = db.Plants.Include(c => c.Category).Include(c => c.PlantCareTypes).ToList();
            return View(plants);
        }

        [HttpGet]
        public ActionResult CreatePartial()
        {
            PlantViewModel plant = new PlantViewModel();
            plant.Categories = db.Categories.ToList();
            plant.PlantCareTypes.Add(new PlantCareType() { PlantCareTypeId = 1 });
            return PartialView("_CreatePlantPartial", plant);
        }

        [HttpPost]

        [ValidateAntiForgeryToken]
        public JsonResult CreatePlant(PlantViewModel vobj)
        {
            if (!ModelState.IsValid)
            {
                vobj.Categories = db.Categories.ToList();
                return Json(new { success = false });
            }
            Plant plant = new Plant
            {
                PlantName = vobj.PlantName,
                FlowerBearing=vobj.FlowerBearing,
                PlantPrice=vobj.PlantPrice,
                Date=vobj.Date,
                CategoryId = vobj.CategoryId,
                PlantCareTypes=vobj.PlantCareTypes

            };

            if (vobj.ProfileFile != null)
            {
                string uniqueFileName = GetFileName(vobj.ProfileFile);
                plant.ImageUrl = uniqueFileName;
            }
            else
            {
                plant.ImageUrl = "noimage.png";
            }

            db.Plants.Add(plant);
            try
            {
                db.SaveChanges();
                return Json(new { success = true, redirectUrl = Url.Action("Index") });
            }
            catch (Exception ex)
            {
                vobj.Categories = db.Categories.ToList();
                return Json(new { success = false });
            }
        }

        private string GetFileName(HttpPostedFileBase file)
        {
            string fileName = "";
            if (file != null)
            {
                fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string path = Path.Combine(Server.MapPath("~/images/"), fileName);
                file.SaveAs(path);
            }
            return fileName;
        }

        [HttpPost]
        public JsonResult DeletePlant(int id)
        {
            Plant plant = db.Plants.Find(id);
            if (plant != null)
            {
                var careTypes = db.PlantCareTypes.Where(s => s.PlantId == id).ToList();
                db.PlantCareTypes.RemoveRange(careTypes);
                db.Entry(plant).State = EntityState.Deleted;
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false, message = "Plant not found." });
        }


        public ActionResult EditPartial(int id)
        {
            var plant = db.Plants.Include(c => c.Category).Include(c => c.PlantCareTypes).Where(s => s.PlantId == id).FirstOrDefault();
            if (plant == null)
            {
                return HttpNotFound("Plant Not found");
            }
            var vObj = new PlantViewModel
            {
                PlantId = plant.PlantId,
                PlantName = plant.PlantName,
                FlowerBearing = plant.FlowerBearing,
                PlantPrice = plant.PlantPrice,
                Date = plant.Date,
                ImageUrl = plant.ImageUrl,
                CategoryId = plant.CategoryId,            
                PlantCareTypes = plant.PlantCareTypes.ToList(),
                Categories = db.Categories.ToList()
            };
            
            return PartialView("_EditPlantPartial", vObj);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult EditPlant(PlantViewModel vobj, string OldImageUrl)
        {
            if (!ModelState.IsValid)
            {
                vobj.Categories = db.Categories.ToList();
                return Json(new { success = false });
            }
            Plant obj = db.Plants
                .Include(a => a.PlantCareTypes)
                .FirstOrDefault(x => x.PlantId == vobj.PlantId);
            if (obj != null)
            {
                obj.PlantName = vobj.PlantName;
                obj.CategoryId = vobj.CategoryId;
                obj.FlowerBearing = vobj.FlowerBearing;
                obj.Date = vobj.Date;
                obj.PlantPrice = vobj.PlantPrice;
                if (vobj.ProfileFile != null)
                {
                    string uniqueFileName = GetFileName(vobj.ProfileFile);
                    obj.ImageUrl = uniqueFileName;
                }
                else
                {
                    obj.ImageUrl = OldImageUrl;
                }

                var existingCareIds = obj.PlantCareTypes.Select(m => m.PlantCareTypeId).ToList();
                var updatedCareIds = vobj.PlantCareTypes.Where(m => m.PlantCareTypeId > 0).Select(m => m.PlantCareTypeId).ToList();
                var caresToRemove = obj.PlantCareTypes.Where(m => !updatedCareIds.Contains(m.PlantCareTypeId)).ToList();
                foreach (var careToRemove in caresToRemove)
                {
                    db.PlantCareTypes.Remove(careToRemove);
                }
                foreach (var item in vobj.PlantCareTypes)
                {
                    if (item.PlantCareTypeId > 0)
                    {
                        var existingCare = obj.PlantCareTypes.FirstOrDefault(m => m.PlantCareTypeId == item.PlantCareTypeId);
                        if (existingCare != null)
                        {
                            existingCare.InsecticideName = item.InsecticideName;
                            existingCare.Fertilizer= item.Fertilizer;
                        }
                    }
                    else
                    {
                        var newCare = new PlantCareType
                        {
                            PlantId = obj.PlantId,
                            InsecticideName = item.InsecticideName,
                            Fertilizer = item.Fertilizer
                        };
                        obj.PlantCareTypes.Add(newCare);
                    }
                }

                try
                {
                    db.SaveChanges();
                    return Json(new { success = true, redirectUrl = Url.Action("Index") });
                }
                catch (Exception ex)
                {

                    vobj.Categories = db.Categories.ToList();
                    return Json(new { success = false });
                }
            }
            return Json(new { success = false, errors = new[] { "Plant not found." } });
        }


    }
}