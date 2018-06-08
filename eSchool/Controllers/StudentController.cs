using eSchool.Data;
using eSchool.Models;
using eSchool.Models.ExamResultViewModel;
using eSchool.Models.StudentViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
                PopulateGrades();
                PopulateStudents();
                PopulateParents();
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
            PopulateGrades();
            PopulateStudents();
            PopulateParents();
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

                var model = (from student in db.Student
                             join Users in db.ApplicationUser on student.Stud_Email equals Users.Email
                             join aspUsers in db.ApplicationUser on student.Parent_Email equals aspUsers.Email
                             join grade in db.Grade on student.Class_Id equals grade.Id
                             orderby grade.Grade_number, grade.Grade_letter ascending
                             select new ListStudentsViewModel
                             {
                                 Id = student.Id,
                                 FirstName = student.FirstName,
                                 LastName = student.LastName,
                                 Stud_Email = student.Stud_Email,
                                 Parent_Email = student.Parent_Email,
                                 User_Id = Users.Id,
                                 Parent_Id = aspUsers.Id,
                                 Parent_FName = aspUsers.FirstName,
                                 Parent_LName = aspUsers.LastName,
                                 Grade_Number = grade.Grade_number,
                                 Grade_Letter = grade.Grade_letter,
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
                PopulateGrades();
                PopulateStudents();
                PopulateParents();
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
            PopulateGrades();
            PopulateStudents();
            PopulateParents();
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

        private void PopulateGrades(object selectedGrade = null)
        {
            var grades = from grade in db.Grade
                           orderby grade.Grade_number, grade.Grade_letter ascending
                           select new
                           {
                               grade.Id,
                               GradeName = string.Format("{0} {1}", grade.Grade_number, grade.Grade_letter)
                           };
            
            ViewBag.Class_Id = new SelectList(grades, "Id", "GradeName", selectedGrade);
        }

        private void PopulateStudents(object selectedStudent = null)
        {
            string studentRole = "2";

            var students = from user in db.ApplicationUser
                           where user.Role.Equals(studentRole)
                           && !(from s in db.Student
                                select s.Stud_Email)
                                .Contains(user.Email)
                           orderby user.FirstName
                           select new
                           {
                               user.Email,
                               StudentInfo = string.Format("{0} {1} -  {2}", user.FirstName, user.LastName, user.Email)
                           };

            ViewBag.Stud_Email = new SelectList(students, "Email", "StudentInfo", selectedStudent);
        }

        private void PopulateParents(object selectedParent = null)
        {
            string parentRole = "3";

            var parents = from user in db.ApplicationUser
                           where user.Role.Equals(parentRole)
                           orderby user.FirstName
                           select new
                           {
                               user.Email,
                               ParentInfo = string.Format("{0} {1} -  {2}", user.FirstName, user.LastName, user.Email)
                           };

            ViewBag.Parent_Email = new SelectList(parents, "Email", "ParentInfo", selectedParent);          
        }
    }
}
