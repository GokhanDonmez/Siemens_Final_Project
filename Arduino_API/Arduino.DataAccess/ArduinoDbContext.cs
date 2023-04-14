using Arduino.Entities;
using Microsoft.EntityFrameworkCore;

namespace Arduino.DataAccess
{
    public class ArduinoDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=(local);Database=ArduinoDb;uid=sa;pwd=123;TrustServerCertificate=True");
        }
        public DbSet<Data>Datas { get; set; }
    }
}