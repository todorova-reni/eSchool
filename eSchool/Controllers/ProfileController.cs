using System.Collections.Generic;
using System.Linq;
using eSchool.Data;
using eSchool.Models;
using eSchool.Models.ProfileViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using eSchool.Models.ExamResultViewModel;

namespace eSchool.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;

        public ProfileController (ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }


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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Display(string userId)
        {
            if(userId != null && CheckAccess())
            {
                userId = userId.ToString();
            } else {
                userId = userManager.GetUserId(User);
            }
            var model = this.db
                .ApplicationUser
                .Where(user => user.Id == userId)
                .Select(user => new DisplayProfileViewModel
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    MobilePhone = user.MobilePhone,
                    Address = user.Address,
                    Active = user.Active,
                    Created_At = user.Created_At,
                    Updated_By = user.Updated_By,
                    Updated_At = user.Updated_At,
                    Role = user.Role == "1" ? "Teacher" : user.Role == "2" ? "Student" : user.Role == "3" ? "Parent" : "Admin"

                })
                .FirstOrDefault();

            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        public IActionResult ExamResults(string Email)
        {
            var userEmail = userManager.GetUserName(this.User);

            var model = (from appUser in db.ApplicationUser
                         join student in db.Student on appUser.Email equals student.Stud_Email
                         join examRes in db.ExamResult on student.Id equals examRes.Student_Id
                         join course in db.Course on examRes.Course_Id equals course.Id
                         join grade in db.Grade on examRes.Grade equals grade.Id
                         where appUser.Email == userEmail
                         orderby examRes.Course_Id, examRes.Created_At descending
                         select new ListExamResultViewModel
                         {
                             Id = examRes.Id,
                             Student_Id = student.Id,
                             Student_FName = student.FirstName,
                             Student_LName = student.LastName,
                             Course_Name = course.Name,
                             Exam_Res = examRes.Grade,
                             Created_At = examRes.Created_At
                         })
                        .ToList();

            return View(model);
        }

        public IActionResult ChildExamResults()
        {
            var userEmail = userManager.GetUserName(this.User);

            var model = (from appUser in db.ApplicationUser
                         join student in db.Student on appUser.Email equals student.Parent_Email
                         join examRes in db.ExamResult on student.Id equals examRes.Student_Id
                         join course in db.Course on examRes.Course_Id equals course.Id
                         where appUser.Email == userEmail
                         orderby examRes.Student_Id, examRes.Created_At descending
                         select new ListExamResultViewModel
                         {
                             Id = examRes.Id,
                             Student_Id = student.Id,
                             Student_FName = student.FirstName,
                             Student_LName = student.LastName,
                             Course_Name = course.Name,
                             Exam_Res = examRes.Grade,
                             Created_At = examRes.Created_At
                         })
                        .ToList();

            return View(model);
        }
    }
}