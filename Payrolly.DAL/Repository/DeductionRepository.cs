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
    public class DeductionRepository : GenericRepository<Deduction>, IDeductionRepository
    {
        public DeductionRepository(ApplicationDbContext context, ILogger<Deduction> logger) : base(context, logger)
        {

        }
    }
}
