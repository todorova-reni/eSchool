using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchool.Models.GradeViewModels
{
    public class ListGradesViewModel
    {
        public int Id { get; set; }

        public int Grade_number { get; set; }
        
        public string Grade_letter { get; set; }
        
        public string Teacher_FName { get; set; }

        public string Teacher_LName { get; set; }

        public int Active { get; set; }

        public DateTime Created_At { get; set; }
    }
}
