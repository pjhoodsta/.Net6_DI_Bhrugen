using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WazeCreditGreen.Data.Repository.IRepository;

namespace WazeCreditGreen.Data.Repository {
    public class UnitOfWork : IUnitOfWork {
        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db) {
            _db = db;
            CreditApplication = new CreditApplicationRepository(_db);
        }

        public ICreditApplicationRepository CreditApplication { get; private set; }

        public void Dispose() {
            _db.Dispose();
        }

        public void Save() {
            _db.SaveChanges();
        }
    }
}