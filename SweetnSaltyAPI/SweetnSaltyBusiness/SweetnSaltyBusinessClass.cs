using SweetnSaltyDbAccess;
using SweetnSaltyModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SweetnSaltyBusiness
{
    public class SweetnSaltyBusinessClass : ISweetnSaltyBusinessClass
    {
        private readonly ISweetnSaltyDbAccessClass _dbAccessClass;
        private readonly IMapper _mapper;

        public SweetnSaltyBusinessClass(ISweetnSaltyDbAccessClass Dbaccess, IMapper mapper)//you need a reference to the DbAccess Layer 
        {
            this._dbAccessClass = Dbaccess;
            this._mapper = mapper;
        }

        public async Task<Flavor> PostFlavor(string flavor)
        {
            SqlDataReader dr = await _dbAccessClass.PostFlavor(flavor);

            if (dr.Read())
                return _mapper.EntityToFlavor(dr);
            else
                return null;
        }

        public async Task<Person> PostPerson(string fname, string lname)
        {
            SqlDataReader dr = await _dbAccessClass.PostPerson(fname, lname);

            if (dr.Read())
                return _mapper.EntityToPerson(dr);
            else
                return null;
        }

        public async Task<Person> GetPerson(string fname, string lname)
        {
            SqlDataReader dr = await _dbAccessClass.GetPerson(fname, lname);

            if (dr.Read())
                return _mapper.EntityToPerson(dr);
            else
                return null;
        }

        public async Task<List<Flavor>> GetAllFlavors()
        {
            SqlDataReader dr = await _dbAccessClass.GetAllFlavors();
            return _mapper.EntityToFlavorList(dr);
        }
    }
}
