using Dtos.Genre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepo
{
    public interface GenreIRepo
    {
       List<GenreDto> AllGenres();
    }
}
