using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WazeCreditGreen.Models;

namespace WazeCreditGreen.Service {
    public interface ICreditValidator {
        Task<(bool, IEnumerable<string>)> PassAllValidations(CreditApplication model);
    }
}