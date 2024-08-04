using OrderCalculatorApp.Data.Interfaces;
using OrderCalculatorApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCalculatorApp.Data.Repositories
{
    /// <summary>
    /// Assumption : Getting all the information from a DB , so considering at asyn tasks
    /// </summary>
    public class ClientRepository : IClientRepository
    {
        private List<Client> _clients = GetMockClients();

        public ClientRepository()
        {

        }

        public async Task<Client?> GetClientAsync(string code)
        {
            return await Task.Run(() =>
            {
                return _clients.FirstOrDefault(x => x.Code.Equals(code, StringComparison.InvariantCultureIgnoreCase));
            }).ConfigureAwait(false);
        }

        private static List<Client> GetMockClients()
        {
            return
            [
                new Client
                {
                    Id = 1,
                    Code = "GA",
                    Name = "GA",
                    TaxPercentage = 1,
                    IsApplyTaxBeforeDiscount = false
                },
                new Client
                {
                    Id = 2,
                    Code = "FL",
                    Name = "FL",
                    TaxPercentage = 1,
                    IsApplyTaxBeforeDiscount = true
                },
                new Client
                {
                    Id = 3,
                    Code = "NY",
                    Name = "NY",
                    TaxPercentage = 1,
                    IsApplyTaxBeforeDiscount = false
                },
                new Client
                {
                    Id = 4,
                    Code = "NM",
                    Name = "NM",
                    TaxPercentage = 1,
                    IsApplyTaxBeforeDiscount = true
                },
                new Client
                {
                    Id = 5,
                    Code = "NV",
                    Name = "NV",
                    TaxPercentage = 1,
                    IsApplyTaxBeforeDiscount = true
                }
            ];
        }
    }
}
