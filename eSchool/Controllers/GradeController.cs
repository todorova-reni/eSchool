using eSchool.Data;
using eSchool.Models;
using eSchool.Models.GradeViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;


namespace eSchool.Controllers
{
    [Authorize]
    public class GradeController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;

        public GradeController(
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
                PopulateTeachers();
                return View();
            }
            else
            {
                return RedirectToAction("Denied", "Access");
            }
        }

        [HttpPost]
        public IActionResult Create(CreateGradeViewModel model)
        {

            ViewData["ReturnUrl"] = ReturnUrl;

            PopulateTeachers();
            if (ModelState.IsValid)
            {
                var userId = this.userManager.GetUserId(this.User);

                var grade = new Grade
                {
                    Grade_number = model.Grade_number,
                    Grade_letter = model.Grade_letter,
                    Teacher_Id = model.Teacher_Id,
                    Active = model.Active,
                    Created_At = DateTime.UtcNow,
                    Created_By = userId,
                    Updated_By = null,
                };

                db.Grade.Add(grade);
                db.SaveChanges();
                return RedirectToAction("List", "Grade");

            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        public IActionResult View(int Id)
        {
            if (CheckAccess())
            {
                var model = this.db
               .Grade
               .Where(grade => grade.Id == Id)
               .Select(grade => new ViewGradeViewModel
               {
                   Grade_number = grade.Grade_number,
                   Grade_letter = grade.Grade_letter,
                   Teacher_Id = grade.Teacher_Id,
                   Active = grade.Active,
                   Created_At = grade.Created_At,
                   Created_by = grade.User.UserName,
                   Updated_By = grade.Updated_By,
                   Updated_At = grade.Updated_At

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
                var model = (from grade in db.Grade
                            join users in db.ApplicationUser on grade.Teacher_Id equals users.Id
                            orderby grade.Grade_number, grade.Grade_letter ascending
                            select new ListGradesViewModel
                            {
                                Id = grade.Id,
                                Grade_number = grade.Grade_number,
                                Grade_letter = grade.Grade_letter,
                                Teacher_FName = users.FirstName,
                                Teacher_LName = users.FirstName,
                                Active = grade.Active,
                                Created_At = grade.Created_At
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

                PopulateTeachers();
                Grade grade = db.Grade.Find(id);
                EditGradeViewModel model = new EditGradeViewModel
                {
                    Grade_number = grade.Grade_number,
                    Grade_letter = grade.Grade_letter,
                    Teacher_Id = grade.Teacher_Id,
                    Active = grade.Active
                };
                return View(model);
            }
            else
            {
                return RedirectToAction("Denied", "Access");
            }
        }

        [HttpPost]
        public ActionResult Edit(Grade grade)
        {
            ViewData["ReturnUrl"] = ReturnUrl;
            PopulateTeachers();
            if (ModelState.IsValid)
            {
                db.Entry(grade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("List");
            }
            //Use automapper to map the objects or assign the data as below                 
            EditGradeViewModel model = new EditGradeViewModel
            {
                Grade_number = grade.Grade_number,
                Grade_letter = grade.Grade_letter,
                Teacher_Id = grade.Teacher_Id,
                Active = grade.Active
            };
            //ViewBag.title_id = new SelectList(db.Titles, "title_id", "Titles", head.title_id);
            return View(model);

        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (CheckAccess())
            {
                var grade = new Grade { Id = id };
                db.Grade.Remove(grade);
                db.SaveChanges();
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Denied", "Access");
            }
        }

        private void PopulateTeachers(object selectedTeacher = null)
        {
            string teacherRole = "1";

            var teachers = from user in db.ApplicationUser
                           where user.Role.Equals(teacherRole)
                           orderby user.FirstName
                           select new
                           {
                               user.Id,
                               FullName = string.Format("{0} {1}", user.FirstName, user.LastName)
                           };

            //ViewBag.Teacher_Id = new SelectList(teachers, "Id", "*", selectedTeacher);

            ViewBag.Teacher_Id = new SelectList(teachers, "Id", "FullName", selectedTeacher);
        }
    }
}
