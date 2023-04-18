namespace Common.DTOs
{
    public class CreateUserDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public decimal UserLatitude { get; set; }
        public decimal UserLongitude { get; set; }
    }
}
