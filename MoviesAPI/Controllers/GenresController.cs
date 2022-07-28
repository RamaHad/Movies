namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenresServices _genresServices;
        public GenresController( IGenresServices genresServices)
        {
            _genresServices = genresServices;
        }

        [HttpGet]
        // api/genres
        public async Task<IActionResult> GetAllGenres()
        {
            var genres = await _genresServices.GetAll();
            return Ok(genres);
        }

        [HttpPost]
        // api/genres
        public async Task<IActionResult> AddGenre([FromBody] AddGenreDto dto)
        {
            var genre = new Genre { Name= dto.Name};
            await _genresServices.Add(genre); 
            return Ok(genre);
        }

        [HttpPut("{id}")]
        // api/genres/id
        public async Task<IActionResult> UpdateGenre ([FromRoute] byte id , [FromBody]AddGenreDto dto )
        {
            var genre = await _genresServices.GetById(id);
            if(genre == null)
                return NotFound($"No Genra was found with id: {id}");

            genre.Name = dto.Name;
             _genresServices.Update(genre);
            return Ok(genre);
        }

        [HttpDelete("{id}")]
        // api/genres/id
        public async Task<IActionResult> DeleteGenre( byte id)
        {
            var genre = await _genresServices.GetById(id);
            if(genre == null)
                return NotFound($"No Genra was found with id: {id}");

            _genresServices.Delete(genre);
            return Ok(genre);
        }
    }
}