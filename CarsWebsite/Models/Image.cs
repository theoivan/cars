namespace API.Models
{
    public class Image
    {
        public int Id { get; set; }

        public int CarId { get; set; }

        public string ImagePath { get; set; }

        public Car Car { get; set; }
    }
}
