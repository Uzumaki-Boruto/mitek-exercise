using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicles.Interfaces;

public interface IVehicleDatabase
{
    public IEnumerable<IEnumerable<Vehicle>> GetRegistrations();
    public IEnumerable<Vehicle> GetVehicles();
    public bool HasMultipleRegistrations(string id);
}
