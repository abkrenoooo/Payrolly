using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Payrolly.DAL.Data;
using Payrolly.DAL.Entities;
using Payrolly.DAL.Interface;
using System.Linq.Expressions;

namespace Payrolly.DAL.Repository
{
    public class GrossPayRepository : GenericRepository<GrossPay>, IGrossPayRepository
    {
        public GrossPayRepository(ApplicationDbContext context, ILogger<GrossPay> logger)
            : base(context, logger)
        {

        }

        public async Task<List<Employee>> GetAllGrossPaysInsideCompany(string companyId, string PayScheduleId, DateTime NextPayDay)
        {
            Expression<Func<Employee, bool>> PaySchedule = x => true;
            Expression<Func<Employee, bool>> PayDay = x => true;
            if (PayScheduleId != null)
            {
                PaySchedule = x => x.PayScheduleId == PayScheduleId;
            }
            DateTime date = DateTime.MinValue;
            if (NextPayDay != date)
            {
                PayDay = x => x.PaySchedule.NextPayDay.Date == NextPayDay.Date;
            }
            return await _context.Employees
                    .Include(z => z.GrossPay).Include(z => z.PaySchedule).Include(c => c.Location).ThenInclude(x => x.Company)
                .Where(c => c.Location.CompanyId == companyId).Where(PaySchedule).Where(PayDay)
                .Distinct().ToListAsync();
        }



        public async Task<bool> UpdateGrossPay(GrossPay grossPays)
        {
            _context.GrossPays.Update(grossPays);
            var x = _context.SaveChanges();
            return x > 0 ? true : false;
        }
    }
}
