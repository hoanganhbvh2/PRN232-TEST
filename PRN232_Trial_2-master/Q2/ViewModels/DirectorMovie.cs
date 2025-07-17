using givenAPI.Models;

namespace Q2.ViewModels
{
    public class DirectorMovie
    {
        public List<Movie> movies { get; set; } = new List<Movie>();
        public List<Director> directors { get; set; } = new List<Director>();

    }
}
