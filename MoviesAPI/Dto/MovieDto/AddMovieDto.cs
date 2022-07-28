namespace MoviesAPI.Dto.MovieDto
{
    public class AddMovieDto
    {
       [MaxLength(250)]
        public string Title { get; set; }

        public int Year { get; set; }
        public double Rate { get; set; }

        [MaxLength(250)]
        public string StoreLine { get; set; }

        public IFormFile ? Poster { get; set; } // file type

        public byte GenreId { get; set; }
       
    }
}
