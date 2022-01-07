using SweetnSaltyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweetnSaltyBusiness
{
    public interface ISweetnSaltyBusinessClass
    {
        Task<Flavor> PostFlavor(string flavor);
        Task<Person> PostPerson(string fname, string lname);
        Task<Person> GetPerson(string fname, string lname);
        Task<List<Flavor>> GetAllFlavors();
    }
}
