
using AgendaMedica.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext: DbContext{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options){

    }

    public DbSet<User> users {get;set;}

}