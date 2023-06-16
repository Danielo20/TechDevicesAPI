using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechDevices_API.Models;

namespace TechDevices_API.Data
{
    public class TechDevices_APIContext : DbContext
    {
        public TechDevices_APIContext (DbContextOptions<TechDevices_APIContext> options)
            : base(options)
        {
        }

        public DbSet<TechDevices_API.Models.Devices> Devices { get; set; } = default!;
    }
}
