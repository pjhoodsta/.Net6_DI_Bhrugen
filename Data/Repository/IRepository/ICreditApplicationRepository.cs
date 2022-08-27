using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WazeCreditGreen.Models;

namespace WazeCreditGreen.Data.Repository.IRepository {
    public interface ICreditApplicationRepository : IRepository<CreditApplication> {
        void Update(CreditApplication obj);
    }
}