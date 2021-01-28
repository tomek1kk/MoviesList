using MoviesList.Models;
using StarWarsApiCSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesList
{
    public interface IGenericRepository<T>
    {
        ICollection<T> GetAll(string type);
        T GetByUrl(string url);
        T GetById(int id, string type);
    }
}
