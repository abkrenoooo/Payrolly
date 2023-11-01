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
    public class PayScheduleRepository : GenericRepository<PaySchedule>, IPayScheduleRepository
    {
        public PayScheduleRepository(ApplicationDbContext context, ILogger<PaySchedule> logger) : base(context, logger)
        {

        }
    }
}
