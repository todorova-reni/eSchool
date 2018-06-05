using eSchool.Data;
using eSchool.Models;
using eSchool.Models.ExamResultViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;

namespace eSchool.Controllers
{
    public class ExamResultController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;

        public ExamResultController(
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
        public IActionResult Add(string studName, int studId)
        {
            if (CheckAccess())
            {
                PopulateCourses();
                ViewData["StudName"] = studName;
                ViewData["studId"] = studId;
                return View();
            }
            else
            {
                return RedirectToAction("Denied", "Access");
            }
        }

        [HttpPost]
        public IActionResult Add(AddExamResultViewModel model)
        {
            if (CheckAccess())
            {
                ViewData["ReturnUrl"] = ReturnUrl;
                if (ModelState.IsValid)
                {
                    var userId = this.userManager.GetUserId(this.User);

                    var examResult = new ExamResult
                    {
                        Student_Id = model.Student_Id,
                        Course_Id = model.Course_Id,
                        Grade = model.Grade,
                        Created_At = DateTime.UtcNow,
                        Created_By = userId,
                        Updated_By = null,
                    };

                    Student student = db.Student.Find(model.Student_Id);
                    var name = student.FirstName + " " + student.LastName;
                    db.ExamResult.Add(examResult);
                    db.SaveChanges();
                    return RedirectToAction("List", "ExamResult", new { studName = name, studId = model.Student_Id });

                }

                // If we got this far, something failed, redisplay form
                return View(model);
            }
            else
            {
                return RedirectToAction("Denied", "Access");
            }
            
        }

        private void PopulateCourses(object selectedCourse = null)
        {
            var courses = from d in db.Course
                                   orderby d.Name
                                   select d;
            ViewBag.Course_ID = new SelectList(courses, "Id", "Name", selectedCourse);
        }

        public IActionResult List(int studId, string studName)
        {
            ViewData["StudName"] = studName;
            ViewData["studId"] = studId;

            var model = (from examRes in db.ExamResult
                         join course in db.Course on examRes.Course_Id equals course.Id
                         join student in db.Student on examRes.Student_Id equals student.Id
                         where examRes.Student_Id == studId
                         orderby examRes.Course_Id, examRes.Created_At descending
                         select new ListExamResultViewModel
                         {
                             Id = examRes.Id,
                             Student_Id = student.Id,
                             Student_FName = student.FirstName,
                             Student_LName = student.FirstName,
                             Course_Name = course.Name,
                             Grade = examRes.Grade,
                             Created_At = examRes.Created_At
                         })
                        .ToList();

            return View(model);
        }

        public IActionResult Delete(int id, int studId, string studName)
        {
            if (CheckAccess())
            {
                var examResult = new ExamResult { Id = id };
                db.ExamResult.Remove(examResult);
                db.SaveChanges();
                return RedirectToAction("List",new { studId, studName});
            }
            else
            {
                return RedirectToAction("Denied", "Access");
            }
        }
    }
}
