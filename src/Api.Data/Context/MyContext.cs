using Microsoft.EntityFrameworkCore;
using Api.Domain.Entities;
using Api.Data.Maping;
using Api.Domain.Dtos.User;
using Api.Data.Mapping;
using Api.Data.Seeds;

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
            modelbuilder.Entity<UfEntity>(new UfMap().Configure);
            modelbuilder.Entity<MunicipioEntity>(new MunicipioMap().Configure);
            modelbuilder.Entity<CepEntity>(new CepMap().Configure);
            modelbuilder.Entity<UserEntity>(new UserMap().Configure);
            modelbuilder.Entity<UserEntity>().HasData(
                new UserEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "Administrador",
                    Email = "email@email.com.br",
                    CreateAt = DateTime.Now,
                    updateAt = DateTime.Now
                });

            UfSeeds.Ufs(modelbuilder);
        }
    }
}