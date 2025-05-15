using Microsoft.EntityFrameworkCore;
using Tinkr.Api.Entities;
namespace Tinkr.Api.Data;

public class TinkrProjectsContext : DbContext
{
    public TinkrProjectsContext(DbContextOptions<TinkrProjectsContext> options)
        : base(options)
    {

    }
    public DbSet<Project> Projects => Set<Project>();
}