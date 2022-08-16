using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WazeCreditGreen.Models;

namespace WazeCreditGreen.Service {
    public class CreditApprovedHigh : ICreditApproved {
        public double GetCreditApproved(CreditApplication creditApplication) {
            // have a different logic to calculate approavl limit
            // we will hardcore to 30% of salary
            return (double)creditApplication.Salary * 0.3;
        }
    }
}