using VodkaEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VodkaServices
{
    public interface ICategoryService
    {
        Task UpdateAsSync(Category category);
        Task UpdateById(string id);
        Task DeleteById(string id);
        Task CreateAsSync(Category category);

        Category GetById(string id);

        IEnumerable<Category> GetAll();
        int GetLastId();
    }
}
