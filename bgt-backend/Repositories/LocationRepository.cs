using System.Collections.Generic;
using BGTBackend.Models;

namespace BGTBackend.Repositories
{
    internal class LocationRepository : Repository<Location>
    {
        public override string TableName { get; } = "locatie";

        public override Dictionary<string, string> DataMap { get; } = new Dictionary<string, string>
        {
            {"locatie.locatie_code", "Id"},
            {"locatie.longtitude", "Longtitude"},
            {"locatie.latitude", "Latitude"}
        };

        public Location Get(string lon, string lat)
        {
            return QueryFirstOrDefault($@"
                SELECT {this.GetSelects()}
                FROM locatie
                WHERE latitude = @lat AND longtitude = @lon
            ", new {lon, lat});
        }

        public Location Add(LocationPost project)
        {
            return Execute(this.GetInserts(), project);
        }
    }
}