using System.Collections.Generic;

namespace WebApplication1.Model
{
    public interface IWorldRepository
    {
        IEnumerable<T> GetAll<T>() where T : class;
    }
}