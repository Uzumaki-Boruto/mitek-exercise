using Vehicles.Interfaces;

namespace Vehicles;

/// <summary>
/// Contains vehicle registrations for plug-in and battery electric vehicles in Washington state.
/// Each vehicle has a unique Id, and each vehicle can be registered multiple times (People moving, or the vehicle being sold).
/// </summary>
public class VehicleRegistry(IVehicleDatabase db)
{
    public IEnumerable<Vehicle> GetVehicles() => db.GetVehicles();

    public IEnumerable<Vehicle> GetRegistrations() => db.GetRegistrations().SelectMany(list => list);

    /// <summary>
    /// Updates the tax for a given vehicle and year.
    /// </summary>
    /// <param name="vehicle">The vehicle whose tax should be calculated.</param>
    /// <param name="year">The tax year.</param>
    /// <returns>The tax amount for the year.</returns>
    public void UpdateTax(Vehicle vehicle, int year)
    {
        var taxCalculatorFactory = new TaxCalculatorFactory(db);
        var taxCalculator = taxCalculatorFactory.GetTaxCalculatorOnYear(year);
        var newTax = taxCalculator.CalculateTax(vehicle);
        vehicle.Tax = newTax;
    }

    /// <summary>
    /// Gets the most popular car model.
    /// </summary>
    /// <returns>The name (Make and model) of the most popular car.</returns>
    public string GetMostPopularModel()
    {
        Dictionary<string, int> counts = new Dictionary<string, int>();
        foreach (var vehicle in db.GetVehicles())
        {
            if (counts.ContainsKey(vehicle.MakeAndModel))
            {
                counts[vehicle.MakeAndModel] = counts[vehicle.MakeAndModel] + 1;
            }
            else
            {
                counts[vehicle.MakeAndModel] = 0;
            }
        }
        int highest = 0;
        string best = "";
        foreach (var kvp in counts)
        {
            if (kvp.Value > highest)
            {
                highest = kvp.Value;
                best = kvp.Key;
            }
        }
        return best;
    }
}