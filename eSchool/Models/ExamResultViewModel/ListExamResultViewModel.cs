using System;

namespace eSchool.Models.ExamResultViewModel
{
    public class ListExamResultViewModel
    {
        public int Id { get; set; }

        public int Student_Id { get; set; }

        public string Student_FName { get; set; }

        public string Student_LName { get; set; }

        public string Parent_Email { get; set; }

        public string Course_Name { get; set; }

        public int Exam_Res { get; set; }

        public int Grade { get; set; }

        public DateTime Created_At { get; set; }
    }
}
