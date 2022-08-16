using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WazeCreditGreen.Models;

namespace WazeCreditGreen.Service {
    public interface IValidationChecker {
        string ErrorMessage { get; }
        bool ValidatorLogic(CreditApplication model);
    }
}