using System;
using System.Collections.Generic;
using BGTBackend.Models;

namespace BGTBackend.Repositories
{
    internal class LocationRepository : Repository<Location>
    {
        protected override Dictionary<string, string> DataMap { get; } = new Dictionary<string, string>
        {
            { "locatie.locatie_code", "Id" },
            { "locatie.longtitude", "Longtitude" },
            { "locatie.latitude", "Latitude" }
        };

        public IEnumerable<Location> GetAll()
        {
            return Query($@"
                SELECT {this.GetSelects()}
                FROM locatie
            ");
        }

        public Location Get(string lon, string lat)
        {
            return QueryFirstOrDefault($@"
                SELECT {this.GetSelects()}
                FROM locatie
                WHERE latitude = @lat AND longtitude = @lon
            ", new { lon, lat });
        }

        public Location Get(int locationId)
        {
            return QueryFirstOrDefault($@"
                SELECT {this.GetSelects()}
                FROM locatie
                WHERE locatie_code = @locationId
            ", new { locationId });
        }

        public Location Add(LocationPost location)
        {
            return Execute(this.GetInserts("locatie"), location);
        }

        public Location Edit(int id, LocationPost location)
        {
            return Execute($@"
                UPDATE locatie
                SET {this.GetUpdates()}
                WHERE locatie_code = @id
            ", new { id });
        }
    }
}