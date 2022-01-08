using SweetnSaltyModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweetnSaltyBusiness
{
    public class Mapper : IMapper
    {
        public Flavor EntityToFlavor(SqlDataReader dr)
        {
            return new Flavor()
            {
                FlavorId = dr.GetInt32(0),
                FlavorName = dr.GetString(1)
            };
        }

        public Person EntityToPerson(SqlDataReader dr)
        {
            return new Person()
            {
                PersonId = dr.GetInt32(0),
                Fname = dr.GetString(1),
                Lname = dr.GetString(2)
            };
        }

        public Person EntityToPersonAndFlavors(SqlDataReader dr)
        {
            Person p = new Person()
            {
                PersonId = dr.GetInt32(0),
                Fname = dr.GetString(1),
                Lname = dr.GetString(2),
                Flavors = new List<Flavor>()
            };
            if (!dr.IsDBNull(3) && !dr.IsDBNull(4))
            {
                do
                {
                    p.Flavors.Add(new Flavor()
                    {
                        FlavorId = dr.GetInt32(3),
                        FlavorName = dr.GetString(4)
                    });
                } while (dr.Read());
            }

            return p;
        }

        public List<Flavor> EntityToFlavorList(SqlDataReader dr)
        {
            List<Flavor> list = new List<Flavor>();

            do
            {
                Flavor flavor = new Flavor()
                {
                    FlavorId = dr.GetInt32(0),
                    FlavorName = dr.GetString(1)
                };
                list.Add(flavor);
            } while (dr.Read());

            return list;
        }
    }
}
