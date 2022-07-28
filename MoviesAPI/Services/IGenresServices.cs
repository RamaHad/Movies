namespace MoviesAPI.Services
{
    public interface IGenresServices
    {
        Task<IEnumerable<Genre>> GetAll();
        Task<Genre> GetById(byte id);
        Task<Genre> Add(Genre genre);
        Genre Update(Genre genre);  //we dont need to async and we will update using update method
        Genre Delete(Genre genre);//we dont need to async and we will delete using remove method
        Task<bool> IsValidGenre (byte id);

    }
}
