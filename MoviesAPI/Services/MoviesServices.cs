
namespace MoviesAPI.Services
{

    public class MoviesServices : IMoviesServices
    {
        private readonly ApplicationDbContext _context;
        public MoviesServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Movie> Add(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            _context.SaveChanges();
            return movie;
        }

        public Movie Delete(Movie movie)
        {
            _context.Remove(movie);
            _context.SaveChanges();
            return movie;
        }

        public async Task<IEnumerable<Movie>> GetAll(byte genraId = 0)

        {
          return  await _context.Movies
               .Where(m=> m.GenreId==genraId || genraId==0 )
               .OrderByDescending(m => m.Rate)
               .Include(m => m.Genre)
               .ToListAsync();

            /*
             get all data about movie with all data about navegation property without transfer to dto
             var movies = await _context.Movies
            .Include(m => m.Genre)
            .ToListAsync();
           */
        }

        public Task<GetMovieDto> GetByGenreId(byte Id)
        {
            throw new NotImplementedException();
        }

     
        public async Task<Movie> GetById(int id)
        {
           return await _context.Movies.Include(m => m.Genre).SingleOrDefaultAsync(m => m.Id == id);

        }

        public async Task<bool> IsValidMovie(int id)
        {
           return await _context.Genres.AnyAsync(g => g.Id == id); // return true or false
        }

        public Movie Update( Movie movie)
        {
            _context.Update(movie);
            _context.SaveChanges();
            return movie;
        }
    }
}
