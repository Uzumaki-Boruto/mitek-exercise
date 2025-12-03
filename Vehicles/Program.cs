namespace Vehicles;

static class Program
{
   /// <summary>
   /// Reports the most popular EV car model in Washington state.
   /// </summary>
   public static void Main(string[] args)
   {
      Console.WriteLine($"Loading data...");
      DateTime start = DateTime.Now;
      var db = new SimpleOptimizedVehicleDatabase();
      Console.WriteLine($"Loaded data after {DateTime.Now - start}");
      var registry = new VehicleRegistry(db);
      Console.WriteLine($"The most popular EV model overall is {registry.GetMostPopularModel()}");
   }
}
