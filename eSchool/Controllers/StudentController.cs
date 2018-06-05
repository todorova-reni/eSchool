using eSchool.Data;
using eSchool.Models;
using eSchool.Models.ExamResultViewModel;
using eSchool.Models.StudentViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace eSchool.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;

        public StudentController(
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
            } else
            {
               return RedirectToAction("Denied", "Access");
            }
        }

        [HttpPost]
        public IActionResult Create(CreateStudentViewModel model)
        {

            ViewData["ReturnUrl"] = ReturnUrl;
            if (ModelState.IsValid)
            {
                var userId = this.userManager.GetUserId(this.User);
                
                var student = new Student
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Stud_Email = model.Stud_Email,
                    Parent_Email = model.Parent_Email,
                    Class_Id = model.Class_Id,
                    Active = model.Active,
                    Created_At = DateTime.UtcNow,
                    Created_By = userId,
                    Updated_By = null,
                };

                db.Student.Add(student);
                db.SaveChanges();                 
                return RedirectToAction("List", "Student");
              
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        public IActionResult View(int Id)
        {
            if (CheckAccess())
            {
                var model = this.db
                   .Student
                   .Where(student => student.Id == Id)
                   .Select(student => new ViewStudentViewModel
                   {
                       Id = student.Id,
                       FirstName = student.FirstName,
                       LastName = student.LastName,
                       Stud_Email = student.Stud_Email,
                       Parent_Email = student.Parent_Email,
                       Class_id = student.Class_Id,
                       Active = student.Active,
                       Created_At = student.Created_At,
                       Created_by = student.User.UserName,
                       Updated_By = student.Updated_By,
                       Updated_At = student.Updated_At

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
                    .Student
                    .Select(student => new ListStudentsViewModel
                    {
                        Id = student.Id,
                        FirstName = student.FirstName,
                        LastName = student.LastName,
                        Stud_Email = student.Stud_Email,
                        Parent_Email = student.Parent_Email,
                        Class_Id = student.Class_Id,
                        Active = student.Active

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

                Student student = db.Student.Find(id);
                EditStudentViewModel model = new EditStudentViewModel
                {
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Stud_Email = student.Stud_Email,
                    Parent_Email = student.Parent_Email,
                    Class_Id = student.Class_Id,
                    Active = student.Active
                };
                return View(model);
            }
            else
            {
                return RedirectToAction("Denied", "Access");
            }
        }

        [HttpPost]
        public ActionResult Edit(Student student)
        {
            ViewData["ReturnUrl"] = ReturnUrl;
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("List");
            }
            //Use automapper to map the objects or assign the data as below                 
            EditStudentViewModel model = new EditStudentViewModel
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                Stud_Email = student.Stud_Email,
                Parent_Email = student.Parent_Email,
                Class_Id = student.Class_Id,
                Active = student.Active
            };
            //ViewBag.title_id = new SelectList(db.Titles, "title_id", "Titles", head.title_id);
            return View(model);
            
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (CheckAccess())
            {
                var student = new Student { Id = id };
                db.Student.Remove(student);
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
