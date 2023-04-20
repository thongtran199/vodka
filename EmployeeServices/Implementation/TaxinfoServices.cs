using VodkaDataAccess;
using VodkaEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VodkaServices.Implementation
{
    public class TaxinfoServices : ITaxinfoService
    {
        private ApplicationDbContext _context;
        public TaxinfoServices(ApplicationDbContext context){
            _context = context;
        }

        public async Task CreateAsSync(Taxinfo taxinfo)
        {
            await _context.Taxinfos.AddAsync(taxinfo);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Taxinfo> GetAll()
        {
            return _context.Taxinfos.ToList();
        }

        public Taxinfo GetById(string id)
        {
            return _context.Taxinfos.Where(x => x.TaxId.Equals(id)).FirstOrDefault();
        }


        public async Task UpdateById(string id)
        {
            var taxinfo = GetById(id);
            if( taxinfo != null)
            {
                _context.Taxinfos.Update(taxinfo);
                await _context.SaveChangesAsync();
            }

        }

        public async Task DeleteById(string id)
        {
            var taxinfo = GetById(id);
            _context.Taxinfos.Update(taxinfo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsSync(Taxinfo taxinfo)
        {
            _context.Taxinfos.Update(taxinfo);
            await _context.SaveChangesAsync();
        }
        public int GetLastId()
        {
            var t = _context.Taxinfos.OrderByDescending(i => i.TaxId).FirstOrDefault();
            return int.Parse(t.TaxId.Replace("T", ""));
        }
    }
}
