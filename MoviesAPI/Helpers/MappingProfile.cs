using AutoMapper;

namespace MoviesAPI.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie, GetMovieDto>(); //to dto with same name
            CreateMap<AddMovieDto, Movie>() //from dto with same name
            .ForMember(src => src.Poster, opt => opt.Ignore());

        }
    }
}
