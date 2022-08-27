using WazeCreditGreen.Data.Repository.IRepository;
using WazeCreditGreen.Models;

namespace WazeCreditGreen.Data.Repository {
    internal class CreditApplicationRepository : Repository<CreditApplication>, ICreditApplicationRepository {
        private readonly ApplicationDbContext _db;
        public CreditApplicationRepository(ApplicationDbContext db) : base(db) {
            _db = db;
        }

        public void Update(CreditApplication obj) {
            _db.CreditApplicationModel.Update(obj);
        }
    }
}