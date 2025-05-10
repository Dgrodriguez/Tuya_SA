using Microsoft.EntityFrameworkCore;

public class MiDbContext : DbContext
{
    public MiDbContext(DbContextOptions<MiDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Data Source=DANIEGO\\AAAA;Initial Catalog=Tuya_Sa;Integrated Security=True;Encrypt=False;Trust Server Certificate=True");
        }
    }


    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Customer)
            .WithMany() // Sin listas en `Customer`
            .HasForeignKey(o => o.CustomerId);
    }



    public bool ProbarConexion()
    {
        try
        {
            using var connection = Database.GetDbConnection();
            connection.Open();
            Console.WriteLine($" Conectado a SQL Server: {connection.DataSource}, Base: {connection.Database}");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($" Error de conexión: {ex.Message}");
            return false;
        }
    }

}