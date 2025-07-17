using Q1.Models;

namespace Q1.ViewModels
{
    public class DirectorViewModel2
    {
        public int id { get; set; }
        public string fullName { get; set; }
        public string gender { get; set; }
        public DateTime dob { get; set; }
        public string dobString { get; set; }
        public string nationality { get; set; }
        public string description { get; set; }
        public virtual ICollection<MovieViewModel> Movies { get; set; } = new List<MovieViewModel>();
    }
}
