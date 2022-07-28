namespace MoviesAPI.Dto.GenreDto
{
    public class AddGenreDto
    {
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
