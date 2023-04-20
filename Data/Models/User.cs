using Microsoft.EntityFrameworkCore;

namespace Data.Models
{
    public class User : BaseEntity<int>
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public City City { get; set; } = null!;
        public int CityId { get; set; }
        public PreciseLocation? PreciseLocation { get; set; } = null!;
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }

    public static partial class EntityExtensions
    {
        public static IQueryable<User> IncludeRelated(this IQueryable<User> query)
        {
            return query
                .Include(u => u.PreciseLocation)
                .Include(u => u.City)
                    .ThenInclude(c => c.County)
                    .ThenInclude(c => c.Country);
        }
    }
}
