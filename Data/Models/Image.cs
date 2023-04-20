namespace Data.Models
{
    public class Image
    {
        public int Id { get; set; }
        public byte[] ImageBytes { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
    }
}
