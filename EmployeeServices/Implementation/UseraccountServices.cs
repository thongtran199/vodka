using VodkaDataAccess;
using VodkaEntities;


namespace VodkaServices.Implementation
{
    public class UseraccountServices : IUseraccountService
    {
        private ApplicationDbContext _context;
        public UseraccountServices(ApplicationDbContext context){
            _context = context;
        }

        public async Task CreateAsSync(Useraccount useraccount)
        {
            await _context.Useraccounts.AddAsync(useraccount);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Useraccount> GetAll()
        {
            return _context.Useraccounts.Where(x => x.IsActive.Trim().Equals("1")).ToList();
        }

        public Useraccount GetById(string id)
        {
            return _context.Useraccounts.Where(x => x.UserId.Equals(id) && x.IsActive.Trim().Equals("1"))
                                      .FirstOrDefault();
        }


        public async Task UpdateById(string id)
        {
            var useraccount = GetById(id);
            if( useraccount != null)
            {
                _context.Useraccounts.Update(useraccount);
                await _context.SaveChangesAsync();
            }

        }

        public async Task DeleteById(string id)
        {
            var useraccount = GetById(id);
            useraccount.IsActive = "0";
            _context.Useraccounts.Update(useraccount);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsSync(Useraccount useraccount)
        {
            _context.Useraccounts.Update(useraccount);
            await _context.SaveChangesAsync();
        }
        public int GetLastId()
        {
            var u = _context.Useraccounts.OrderByDescending(i => i.UserId).FirstOrDefault();
            return int.Parse(u.UserId.Replace("U", ""));
        }
    }
}
