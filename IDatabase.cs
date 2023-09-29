using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    internal interface IDatabase
    {
        public interface IDatabase
        {
            List<Airport> SelectAllAirports();
            Airport SelectAirport(string id);
            bool InsertAirport(Airport airport);
            bool UpdateAirport(Airport airport);
            bool DeleteAirport(string id);
        }
    }
}
