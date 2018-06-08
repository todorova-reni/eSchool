namespace eSchool.Models.StudentViewModels
{
    public class ListStudentsViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Stud_Email { get; set; }

        public string Parent_Email { get; set; }

        public string User_Id { get; set; }

        public string Parent_Id { get; set; }

        public string Parent_FName { get; set; }

        public string Parent_LName { get; set; }

        public int Grade_Number { get; set; }

        public string Grade_Letter { get; set; }

        public int Active { get; set; }
    }
}
