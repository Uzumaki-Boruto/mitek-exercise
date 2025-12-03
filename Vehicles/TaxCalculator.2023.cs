using Vehicles.Interfaces;

namespace Vehicles;

public class TaxCalculator_2023 : ITaxCalculator
{
    const string EvType_PlugInHybrid = "Plug-in Hybrid Electric Vehicle (PHEV)";
    const string EvType_BatteryElectric = "Battery Electric Vehicle (BEV)";

    const decimal Tax_10 = 10.0m;
    const decimal Tax_100 = 100.0m;
    public decimal CalculateTax(Vehicle vehicle)
    {
        if (vehicle.EvType == EvType_PlugInHybrid)
        {
            return Tax_100;
        }
        else if (vehicle.EvType == EvType_BatteryElectric)
        {
            return Tax_10;
        }
        return 0;
    }
}
