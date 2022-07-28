namespace MoviesAPI.Services
{
    public interface IMoviesServices
    {      
        Task<Movie> Add(Movie movie);
        Movie Delete(Movie movie); // we dont need to async
        Task<IEnumerable<Movie>> GetAll(byte genraId =0);
        Task<GetMovieDto> GetByGenreId(byte Id);
        Task<Movie> GetById(int id);
        Movie Update( Movie movie);// we dont need to async
        Task<bool> IsValidMovie(int id);
    }
}
