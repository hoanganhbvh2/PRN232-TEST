using Q1.Models;

namespace Q1.ViewModels
{
    public class MovieViewModel
    {
        public int id { get; set; }

        public string title { get; set; } = null!;

        public DateTime? releaseDate { get; set; }
        public int realeaseYear { get; set; }

        public string? description { get; set; }

        public string language { get; set; } = null!;

        public int? producerId { get; set; }

        public int? directorId { get; set; }
        public string? producerName { get; set; }
        public string? directorName { get; set; }        

        public ICollection<Genre> genres { get; set; } = new List<Genre>();

        public ICollection<Star> stars { get; set; } = new List<Star>();
    }
}
