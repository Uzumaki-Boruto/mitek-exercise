using Vehicles.Interfaces;

namespace Vehicles;

public class TaxCalculator_2025(IVehicleDatabase vehicleDatabase) : ITaxCalculator
{
    const string EvType_PlugInHybrid = "Plug-in Hybrid Electric Vehicle (PHEV)";
    const string EvType_BatteryElectric = "Battery Electric Vehicle (BEV)";

    public decimal CalculateTax(Vehicle vehicle)
    {
        var taxBase = vehicle.EvType switch
        {
            EvType_BatteryElectric when Is_CAFV_Eligibility(vehicle.CAFV_Eligibility) => 15.0m,
            EvType_BatteryElectric when !Is_CAFV_Eligibility(vehicle.CAFV_Eligibility) => 30.0m,
            EvType_PlugInHybrid when Is_CAFV_Eligibility(vehicle.CAFV_Eligibility) => 50.0m,
            EvType_PlugInHybrid when !Is_CAFV_Eligibility(vehicle.CAFV_Eligibility) => 150.0m,
            _ => 0m
        };
        if (vehicle.City.Equals("Seattle"))
        {
            taxBase += 7.0m;
        }
        if (vehicleDatabase.HasMultipleRegistrations(vehicle.Id))
        {
            taxBase -= 10.0m;
        }
        return Math.Max(0m, taxBase);
    }

    private static bool Is_CAFV_Eligibility(string cafv)
    {
        return cafv.Equals("Clean Alternative Fuel Vehicle Eligible");
    }
}
