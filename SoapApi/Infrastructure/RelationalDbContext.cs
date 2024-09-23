using Microsoft.EntityFrameworkCore;
using SoapApi.Infrastructure.Entities;

namespace SoapApi.Infrastructure;

public class RelationalDbContext : DbContext{
    public RelationalDbContext(DbContextOptions<RelationalDbContext> options) : base(options){

    }
    public DbSet<UserEntity> Users {get; set; }



    public DbSet<BookEntity> Books { get; set; }  // bd para tabla Books

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }

}