using System;
using System.Collections.Generic;

namespace SweetnSaltyModels
{
    public class Person
    {
        public int PersonId { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public List<Flavor> Flavors { get; set; }
    }
}
