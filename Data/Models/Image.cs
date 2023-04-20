namespace Data.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string ImageBase64 { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
    }
}
