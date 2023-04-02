using VodkaDataAccess;
using VodkaEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VodkaServices.Implementation
{
    public class CategoryServices : ICategoryService
    {
        private ApplicationDbContext _context;
        public CategoryServices(ApplicationDbContext context){
            _context = context;
        }

        public async Task CreateAsSync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories.Where(x => x.IsActive.Trim().Equals("1")).ToList();
        }

        public Category GetById(string id)
        {
            return _context.Categories.Where(x => x.CatId.Equals(id) && x.IsActive.Trim().Equals("1"))
                                      .FirstOrDefault();
        }


        public async Task UpdateById(string id)
        {
            var category = GetById(id);
            if( category != null)
            {
                _context.Categories.Update(category);
                await _context.SaveChangesAsync();
            }

        }

        public async Task DeleteById(string id)
        {
            var category = GetById(id);
            category.IsActive = "0";
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsSync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }
    }
}
