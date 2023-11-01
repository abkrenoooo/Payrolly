using Microsoft.Extensions.Logging;
using Payrolly.DAL.Data;
using Payrolly.DAL.Entities;
using Payrolly.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.DAL.Repository
{
    public class TaxWithholdingRepository : GenericRepository<TaxWithholding>, ITaxWithholdingRepository
    {
        public TaxWithholdingRepository(ApplicationDbContext context, ILogger<TaxWithholding> logger)
            : base(context, logger)
        {

        }
        public async Task<bool> UpdateTaxWithholdingPay(TaxWithholding TaxWithholding)
        {

            _context.TaxWithholdings.Update(TaxWithholding);
            //_context.Entry(item).State = EntityState.Modified;

            var x = _context.SaveChanges();
            return x > 0 ? true : false;
        }
    }
}
