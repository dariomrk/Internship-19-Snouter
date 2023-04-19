namespace Contracts.Requests
{
    public class CreateCountyRequest
    {
        public string Name { get; set; }
        public ICollection<CreateCityRequest> Cities { get; set; } = new List<CreateCityRequest>();

    }
}
