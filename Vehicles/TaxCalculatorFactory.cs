using Vehicles.Interfaces;

namespace Vehicles;

public class TaxCalculatorFactory(IVehicleDatabase vehicleDatabase)
{
    public ITaxCalculator GetTaxCalculatorOnYear(int year)
    {
        return year switch
        {
            2023 => new TaxCalculator_2023(),
            2024 => new TaxCalculator_2024(),
            2025 => new TaxCalculator_2025(vehicleDatabase),
            _ => throw new ArgumentException($"Can't calculate tax for year {year}", nameof(year)),
        };
    }
}