using Microsoft.EntityFrameworkcore;
using SoapApi.Infrastrcture.Entities;

namespace SoapApi.Infrastrcture;

public class RelationalDbContext : DbContext
{
    public RelationalDbContext(DbContextOptions<RelationalDbContext> options) :base(options)
}
{
    public DbSet<UserEntity> Users {get; set;}
}