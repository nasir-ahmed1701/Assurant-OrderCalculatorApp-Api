using OrderCalculatorApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrderCalculatorApp.Data.Interfaces
{
    public interface IClientRepository
    {
        public Task<Client?> GetClientAsync(string code);
    }
}
