using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Immutable;

namespace Lab2
{
    public class Database : IDatabase
    {
        // Collection to store airports.
        public ObservableCollection<Airport> airports { get; set; }

        // File-related variables.
        string filename = "airports.db.txt";
        string mainDir = FileSystem.Current.AppDataDirectory;

        // JSON serialization options.
        private JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };

        public Database()
        {
            // Constructor initializes and sets up the database.
            // It also ensures that the file exists or creates it if not.
            WriteAirports(); // This writes the initial empty collection to the file.
            filename = String.Format("{0}/{1}", mainDir, filename);
        }

        // Reads and deserializes airport data from the file.
        public ObservableCollection<Airport> SelectAllAirports()
        {
            string jsonAirports;

            // Use the JSON deserializer to check if the file exists.
            if (File.Exists(filename))
            {
                jsonAirports = File.ReadAllText(filename);
                airports = JsonSerializer.Deserialize<ObservableCollection<Airport>>(jsonAirports);
            }
            else
            {
                // If the file doesn't exist, create a new empty collection and save it.
                ObservableCollection<Airport> airports = new();
                jsonAirports = JsonSerializer.Serialize(airports, options);
                File.WriteAllText(filename, jsonAirports);
            }

            return airports;
        }

        // Writes the airport data to the JSON file.
        public void WriteAirports()
        {
            string jsonAirports;
            try
            {
                jsonAirports = JsonSerializer.Serialize(airports, options);
                File.WriteAllText(filename, jsonAirports);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving airports to file: {ex.Message}");
            }
        }

        // Returns an airport with a given ID, or null if not found.
        public Airport SelectAirport(String id)
        {
            return airports.FirstOrDefault(a => a.Id == id);
        }

        // Deletes an airport with a given ID from the collection.
        public bool DeleteAirport(String id)
        {
            // Check if the airport with the specified ID exists.
            var airportToDelete = airports.FirstOrDefault(a => a.Id == id);

            if (airportToDelete == null)
            {
                return false; // Airport not found.
            }

            // Remove the airport from the collection.
            airports.Remove(airportToDelete);
            return true; // No errors.
        }

        // Inserts a new airport into the collection.
        public bool InsertAirport(Airport airport)
        {
            try
            {
                // Check if an airport with the same ID already exists.
                if (airports.Any(a => a.Id == airport.Id))
                {
                    return false; // Airport with the same ID already exists.
                }

                // Add the airport to the collection.
                airports.Add(airport);
                return true; // Airport added successfully.
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during insertion.
                Console.WriteLine($"Error while adding airport: {ex.Message}");
                return false; // Airport insertion failed due to an exception.
            }
        }

        // Updates an existing airport's information.
        public bool UpdateAirport(Airport airport)
        {
            foreach (var existingAirport in airports)
            {
                if (airport.Id == existingAirport.Id)
                {
                    // Update the airport with the new information.
                    existingAirport.City = airport.City;
                    existingAirport.DateVisited = airport.DateVisited;
                    existingAirport.Rating = airport.Rating;

                    return true; // Airport updated successfully.
                }
            }

            return false; // Airport not found.
        }
    }
}
