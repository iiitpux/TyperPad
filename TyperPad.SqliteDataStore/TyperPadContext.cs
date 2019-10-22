using Microsoft.EntityFrameworkCore;
using TyperPad.SqliteDataStore.Dto;

namespace TyperPad.SqliteDataStore
{
    public class TyperPadContext : DbContext
    {
        public DbSet<StickSectorDto> StickSectors { get; set; }
        public DbSet<LevelDto> Levels { get; set; }
        public DbSet<KeyItemDto> KeyItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=typerpad.db");
    }
}