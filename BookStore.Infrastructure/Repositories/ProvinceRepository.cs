using BookStore.Common.Models;
using BookStore.Common.Models.Interfaces;
using BookStore.Infrastructure.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Repositories;

public class ProvinceRepository : GenericRepository<Province>, IProvinceRepository
{
    public ProvinceRepository(ApplicationDBContext context, ILogger logger) : base(context, logger)
    {
    }
}
