namespace MoviesAPI.Dto.MovieDto
{
    public class GetMovieDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int Year { get; set; }
        public double Rate { get; set; }

        public string StoreLine { get; set; }

        public byte[] Poster { get; set; }

        public byte GenreId { get; set; }
        public string GenreName { get; set; } // genre name from genre table
    }
}
