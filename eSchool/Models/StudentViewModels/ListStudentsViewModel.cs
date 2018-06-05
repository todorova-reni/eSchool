namespace eSchool.Models.StudentViewModels
{
    public class ListStudentsViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Stud_Email { get; set; }

        public string Parent_Email { get; set; }

        public int Class_Id { get; set; }

        public int Active { get; set; }
    }
}
