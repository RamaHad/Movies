using AutoMapper;
using Microsoft.AspNetCore.Http;
 using Microsoft.AspNetCore.Mvc;
namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {

        private readonly IMoviesServices _moviesServices;
        private readonly IGenresServices _genresServices;
        private readonly IMapper _mapper;


        public MoviesController(IMoviesServices moviesServices, IGenresServices genresServices, IMapper mapper)
        {
            _moviesServices = moviesServices;
            _genresServices = genresServices;
            _mapper = mapper;
        }

        // type of file that you want
        private List<String> _allowedExtenstions = new List<string>() { ".jpg", ".png" };

        // size of file that you want
        private long _maxAlowedPosterSize = 1048576;

        [HttpGet]
        // api/movies
        public async Task<IActionResult> GetAllMovies()
        {
           var movies= await _moviesServices.GetAll();
            var data = _mapper.Map<IEnumerable<GetMovieDto>>(movies);
                return Ok(data);
        }

        [HttpGet("{id}")]
        // api/movies/id
        public async Task<IActionResult> GetMovieById(int id)
        {
            var movie = await _moviesServices.GetById(id);
            if (movie == null)
            {
                return NotFound($"There is no movie with id: {id}");
            }
            var data = _mapper.Map<GetMovieDto>(movie); 
            return Ok(data);
        }

        [HttpGet("GetByGenreId")]
        //GetByGenreId is name of this end point => to can using more than one get with parameter
        //api/movies/GetByGenreId => id from params or as quiryString in postman
        public async Task<IActionResult> GetByGenreId(byte genreId)
        {
            var movies = await _moviesServices.GetAll(genreId);
            var data = _mapper.Map<IEnumerable<GetMovieDto>>(movies);
            return Ok(data);
        }

        [HttpPost]
        // api/movies
        public async Task<IActionResult> AddMovie([FromForm] AddMovieDto dto)
        {
            // if the Extenstion of file is False
            if (!(_allowedExtenstions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower())))
            {
                return BadRequest("only .jpg and .png are allowed!");
            }

            // if the size of file is False
            if(dto.Poster.Length>_maxAlowedPosterSize)
            {
                return BadRequest("Max allowed size for boster is 1MB");
            }

            //genre id not found 
            var isValidGenre = await _genresServices.IsValidGenre(dto.GenreId);
            if (! isValidGenre)
                return BadRequest("Invalid genre id");

            using var dataStream = new MemoryStream(); // from IFormFile to Byte[]
            await dto.Poster.CopyToAsync(dataStream); // from IFormFile to Byte[]

            //add the movie
            var movie = _mapper.Map<Movie>(dto);
            movie.Poster = dataStream.ToArray();
            _moviesServices.Add(movie);
            return Ok(movie);
        }

        [HttpPut ("{id}")]
        public async Task<IActionResult>  UpdateMovie(int id, [FromForm] UpdateDto dto)
        {
            //genre id not found 
            var isValidGenre = await _genresServices.IsValidGenre(dto.GenreId);
            if (!isValidGenre)
                return BadRequest("Invalid genre id");


            // find the movie that we want to update it
            var movie = await _moviesServices.GetById(id); 
            if (movie == null)
                return NotFound($"No movie was found with id = {id}");
           
            // if we will update the poster
            if( dto.Poster != null)
            {

                // if the Extenstion of file is False
                if (!(_allowedExtenstions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower())))
                {
                    return BadRequest("only .jpg and .png are allowed!");
                }

                // if the size of file is False
                if (dto.Poster.Length > _maxAlowedPosterSize)
                {
                    return BadRequest("Max allowed size for Poster is 1MB");
                }
                 
                using var dataStream = new MemoryStream(); // from IFormFile to Byte[]
                await dto.Poster.CopyToAsync(dataStream); // from IFormFile to Byte[]
                movie.Poster = dataStream.ToArray();
            }

            //update process
            movie.Title = dto.Title;
            movie.Year = dto.Year;
            movie.GenreId = dto.GenreId;    
            movie.StoreLine = dto.StoreLine;
            movie.Rate = dto.Rate;
            _moviesServices.Update(movie);
            return Ok(movie);
        }

        [HttpDelete ("{id}")]
        // api/movies/id
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _moviesServices.GetById(id); 
            if(movie == null)
            {
                return NotFound($"No movie was found with id = {id}");
            }
            _moviesServices.Delete(movie);
            return Ok(movie);
        }

    }
}
