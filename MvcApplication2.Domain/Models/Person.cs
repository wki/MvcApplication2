using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcApplication2.Domain.Models
{
    public class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Hobby { get; set; }
        public DateTime? Updated { get; set; }

        [NotMapped]
        public string Nickname { get; set; }
    }
}