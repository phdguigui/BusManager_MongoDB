using BusManager.Data;
using BusManager.Model.DTO;
using BusManager.Model.Entities;
using MongoDB.Driver;

namespace BusManager.Repository
{
    public static class DriverRepository
    {
        private static readonly IMongoCollection<Driver> _driverCollection;
        private static readonly IMongoCollection<Trip> _tripCollection;

        static DriverRepository()
        {
            ApplicationContext context = new();
            var database = context.database;
            _driverCollection = database.GetCollection<Driver>("Drivers");
            _tripCollection = database.GetCollection<Trip>("Trips");
        }

        public static List<Driver> GetAllDrivers()
        {
            return _driverCollection.Find(FilterDefinition<Driver>.Empty).ToList();
        }

        public static List<Driver> GetActiveDrivers()
        {
            var filter = Builders<Driver>.Filter.Eq(driver => driver.Active, true);
            return _driverCollection.Find(filter).ToList();
        }

        public static Driver GetDriverByCPF(string cpf)
        {
            var filter = Builders<Driver>.Filter.Eq(driver => driver.CPF, cpf);
            return _driverCollection.Find(filter).FirstOrDefault();
        }

        public static bool AddDriver(Driver driver)
        {
            _driverCollection.InsertOne(driver);
            return true;
        }

        public static bool UpdateDriver(Driver driver)
        {
            var filter = Builders<Driver>.Filter.Eq(d => d.Id, driver.Id);
            var result = _driverCollection.ReplaceOne(filter, driver);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public static bool DeleteDriver(Driver driver)
        {
            var filter = Builders<Driver>.Filter.Eq(d => d.Id, driver.Id);
            var result = _driverCollection.DeleteOne(filter);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public static List<DriverTripRanking> GetDriverRanking()
        {
            var driverRankings = new List<DriverTripRanking>();

            var drivers = _driverCollection.Find(driver => true).ToList();

            foreach (var driver in drivers)
            {
                var tripCount = _tripCollection.CountDocuments(t => t.DriverId == driver.Id);

                driverRankings.Add(new DriverTripRanking()
                {
                    Name = driver.Name,
                    Surname = driver.Surname,
                    HireDate = driver.HireDate,
                    TripCount = (int)tripCount
                });
            }

            driverRankings = driverRankings.OrderByDescending(d => d.TripCount).ToList();

            return driverRankings;
        }
    }
}
