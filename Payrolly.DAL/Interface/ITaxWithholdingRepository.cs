﻿using Payrolly.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.DAL.Interface
{
    public interface ITaxWithholdingRepository : IGenericRepository<TaxWithholding>
    {
        public Task<bool> UpdateTaxWithholdingPay(TaxWithholding grossPays);
    }
}
