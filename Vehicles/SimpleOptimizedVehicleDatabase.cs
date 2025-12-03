using System.Reflection;
using Vehicles.Interfaces;

namespace Vehicles;

public class SimpleOptimizedVehicleDatabase : IVehicleDatabase
{
    private readonly Dictionary<string, List<Vehicle>> vehiclesDict;

    public SimpleOptimizedVehicleDatabase()
    {
        vehiclesDict = [];
        string csvPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "ev.csv");
        Load(csvPath);
    }

    /// <summary>
    /// Loads the vehicle data from disk. 
    /// NOTE: the registrations are in chronological order with newest first. That is, the first occurrence of a vehicle
    ///       will be the current registration for that vehicle. Any subsequent occurrence is a previous owner etc.
    /// </summary>
    /// <param name="csvPath"></param>
    private void Load(string csvPath)
    {
        using var reader = new StreamReader(csvPath);
        string? row;
        reader.ReadLine(); // skip header lines
        while ((row = reader.ReadLine()) is not null)
        {
            var columns = row.Split(',');
            string id = columns[0];
            string county = columns[1];
            string city = columns[2];
            string state = columns[3];
            int modelYear = int.Parse(columns[5]);
            string make = columns[6];
            string model = columns[7];
            string evType = columns[8];
            string cafv_Eligibility = columns[9];
            int evRange = int.Parse(columns[10]);
            var entry = new Vehicle
            {
                Id = id,
                State = state,
                City = city,
                County = county,
                Make = make,
                Model = model,
                ModelYear = modelYear,
                EvType = evType,
                EvRange = evRange,
                CAFV_Eligibility = cafv_Eligibility,
            };

            if (vehiclesDict.TryGetValue(id, out List<Vehicle> vehicles))
            {
                vehicles.Add(entry);
            }
            else
            {
                vehiclesDict[id] = [entry];
            }
        }
    }

    public IEnumerable<IEnumerable<Vehicle>> GetRegistrations() => vehiclesDict.Select(x => x.Value);

    public IEnumerable<Vehicle> GetVehicles() => vehiclesDict.Select(x => x.Value.FirstOrDefault());

    public bool HasMultipleRegistrations(string id) => vehiclesDict.TryGetValue(id, out var registration) && registration.Count > 1;
}
