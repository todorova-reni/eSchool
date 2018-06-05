using eSchool.Data;
using eSchool.Models;
using eSchool.Models.CourseViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace eSchool.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;

        public CourseController(
            ApplicationDbContext db,
            UserManager<ApplicationUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;

        }
        private object ReturnUrl;

        private bool CheckAccess()
        {
            var userId = this.userManager.GetUserId(this.User);
            ApplicationUser user = userManager.FindByIdAsync(userId).Result;
            if (user.Role == "0" || user.Role == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (CheckAccess())
            {
                return View();
            }
            else
            {
                return RedirectToAction("Denied", "Access");
            }
        }

        [HttpPost]
        public IActionResult Create(CreateCourseViewModel model)
        {

            ViewData["ReturnUrl"] = ReturnUrl;
            if (ModelState.IsValid)
            {
                var userId = this.userManager.GetUserId(this.User);

                var course = new Course
                {
                    Name = model.Name,
                    Description = model.Description,
                    Active = 1,
                    Created_At = DateTime.UtcNow,
                    Created_By = userId,
                    Updated_By = null,
                };

                db.Course.Add(course);
                db.SaveChanges();
                return RedirectToAction("List", "Course");

            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        public IActionResult View(int Id)
        {
            if (CheckAccess())
            {
                var model = this.db
               .Course
               .Where(course => course.Id == Id)
               .Select(course => new ViewCourseViewModel
               {
                   Name = course.Name,
                   Description = course.Description,
                   Active = course.Active,
                   Created_At = course.Created_At,
                   Created_by = course.User.UserName,
                   Updated_By = course.Updated_By,
                   Updated_At = course.Updated_At

               })
               .FirstOrDefault();

                if (model == null)
                {
                    return NotFound();
                }

                return View(model);
            }
            else
            {
                return RedirectToAction("Denied", "Access");
            }

        }

        public IActionResult List()
        {
            if (CheckAccess())
            {
                var model = this.db
                    .Course
                    .Select(course => new ListCoursesViewModel
                    {
                        Id = course.Id,
                        Name = course.Name,
                        Active = course.Active

                    })
                    .ToList();

                if (model == null)
                {
                    return NotFound();
                }
                return View(model);
            }
            else
            {
                return RedirectToAction("Denied", "Access");
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (CheckAccess())
            {
                if (id == 0)
                {
                    return NotFound();
                }

                Course course = db.Course.Find(id);
                EditCourseViewModel model = new EditCourseViewModel
                {
                    Name = course.Name,
                    Description = course.Description,
                    Active = course.Active
                };
                return View(model);
            }
            else
            {
                return RedirectToAction("Denied", "Access");
            }
        }

        [HttpPost]
        public ActionResult Edit(Course course)
        {
            ViewData["ReturnUrl"] = ReturnUrl;
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("List");
            }
            //Use automapper to map the objects or assign the data as below                 
            EditCourseViewModel model = new EditCourseViewModel
            {
                Name = course.Name,
                Description = course.Description,
                Active = course.Active
            };
            //ViewBag.title_id = new SelectList(db.Titles, "title_id", "Titles", head.title_id);
            return View(model);

        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (CheckAccess())
            {
                var course = new Course { Id = id };
                db.Course.Remove(course);
                db.SaveChanges();
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Denied", "Access");
            }
        }
    }
}
