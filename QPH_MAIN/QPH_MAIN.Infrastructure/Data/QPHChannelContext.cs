using Microsoft.EntityFrameworkCore;
using QPH_MAIN.Core.Entities;
using System.Reflection;

namespace QPH_MAIN.Infrastructure.Data
{
    public partial class QPHChannelContext : DbContext
    {
        public QPHChannelContext() {}

        public QPHChannelContext(DbContextOptions<QPHContext> options) : base(options) {}
        public virtual DbSet<Channel> Channel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}