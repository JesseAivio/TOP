using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TOP.Library.Data.models;

namespace TOP.API.Data.Context
{
    public class TOPContext : DbContext
    {
        public TOPContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<VocationalQualificationUnit> VocationalQualificationUnits { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Company> Companys { get; set; }
        public DbSet<TOPTable> TOPs { get; set; }
        public DbSet<Account> Accounts { get; set; }
    }
}
