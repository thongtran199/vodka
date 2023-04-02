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
        private VodkadatabaseContext _context;
        public CategoryServices(VodkadatabaseContext context){
            _context = context;
        }

        public async Task CreateAsSync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories.Where(x => x.IsActive.Equals("1")).ToList();
        }

        public Category GetById(string id)
        {
            return _context.Categories.Where(x => x.CatId.Equals(id)).FirstOrDefault();
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
            await UpdateAsSync(category);
            //_context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsSync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }
    }
}
