using Vehicles.Interfaces;

namespace Vehicles;

public class TaxCalculator_2024 : ITaxCalculator
{
    const string EvType_PlugInHybrid = "Plug-in Hybrid Electric Vehicle (PHEV)";
    const string EvType_BatteryElectric = "Battery Electric Vehicle (BEV)";
    const decimal Tax_200 = 200.0m;
    const decimal Tax_20 = 20.0m;
    const decimal Tax_50 = 50.0m;

    public decimal CalculateTax(Vehicle vehicle)
    {
        if (vehicle.EvType == EvType_PlugInHybrid)
        {
            return Tax_200;
        }
        else if (vehicle.EvType == EvType_BatteryElectric)
        {
            if (vehicle.EvRange >= 100)
            {
                return Tax_20;
            }
            return Tax_50;
        }
        return 0;
    }
}
