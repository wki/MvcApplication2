using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcApplication2.Repository.EF
{
    public class BusinessCardState
    {
        public BusinessCardState()
        {
        }

        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string Title { get; set; }
        public string Phone { get; set; }
        public string EMail { get; set; }
        public int Status { get; set; } // TODO: convert into an enum
    }
}
