using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;


namespace Lab2
{
    public class BusinessLogic : IBusinessLogic


    {
        // The BusinessLogic class talks to the Database
        private Database database {  get; set; }

        // Constructor
        public ObservableCollection<Airport> Airports
        {
           get { return database.SelectAllAirports();  }
        }

        public BusinessLogic()
        {
            database = new Database();
        }

        // Check that each field is valid, no duplicates
        public bool AddAirport(string id, string city, DateTime dateVisited, int rating)
        {
            // Validate the input
            if (id.Length < 3 || id.Length > 4)
            {
                return false;
            }

            if (city.Length > 25)
            {
                return false;
            }

            if (rating < 1 || rating > 5)
            {
                return false;
            }

            // Call the database to add the airport
            bool result = database.InsertAirport(new Airport(id, city, dateVisited, rating));
            return result;
        }
        public bool DeleteAirport(string id)
        {
            // Call the database to delete the airport
            return database.DeleteAirport(id);

        }
        public bool EditAirport(string id, string city, DateTime dateVisited, int rating)
        {
            // Create a new airport object and return updated airport
            Airport airport = new Airport(id, city, dateVisited, rating);
            return database.UpdateAirport(airport);

        }


        public Airport FindAirport(string id)
        {
            return database.SelectAirport(id);
        }

        public string CalculateStatistics()
        {
            // set a variable to get the count of how many airports have been added
            int airportCount = GetAirports().Count();
            int bronze = 42;
            int silver = 84;
            int gold = 125;

            // Check the number of airports for respective medal
            if (airportCount < bronze)
            {
                return (airportCount + " airports visited; " + (bronze - airportCount) + " airports remaining until reaching bronze");
            }
            else if (airportCount < silver)
            {
                return (airportCount + " airports visited; " + (silver - airportCount) + " airports remaining until reaching silver");
            }
            else if (airportCount < gold)
            {
                return (airportCount + " airports visited; " + (gold - airportCount) + " airports remaining until reaching gold");
            }
            else
            {
                return (airportCount + " airports visited; Congrats you have visited all of the Airports in Wisconsin");
            }
        }

        public ObservableCollection<Airport> GetAirports()
        {
            // This is the businesslogic talking to the database
            return database.SelectAllAirports();
        }
    }
}
