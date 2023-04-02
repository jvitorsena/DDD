using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore;

namespace Api.Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        public MyContext CreateDbContext(string[] args)
        {
            string connectionString = "server=localhost;user=root;password=root_pwd;database=CSharpDDD";
            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
            optionsBuilder.UseMySql(connectionString, Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.31-mysql"));
            return new MyContext(optionsBuilder.Options);
        }
    }
}