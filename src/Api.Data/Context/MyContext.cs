using Microsoft.EntityFrameworkCore;
using Api.Domain.Entities;
using Api.Data.Maping;

namespace Api.Data.Context
{
    public class MyContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);
            modelbuilder.Entity<UserEntity>(new UserMap().Configure);
        }
    }
}