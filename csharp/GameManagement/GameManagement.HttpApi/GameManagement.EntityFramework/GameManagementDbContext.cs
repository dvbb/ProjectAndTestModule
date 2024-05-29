using GameManagement.EntityFramework.Mappings;
using GameMenagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;

namespace GameManagement.EntityFramework
{
    public class GameManagementDbContext : DbContext
    {
        public GameManagementDbContext(DbContextOptions<GameManagementDbContext> options) : base(options)
        {
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Character> Characters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1.通过反射获得所有类型
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // 2.手动加载全部类型
            if (false)
            {
                modelBuilder.ApplyConfiguration(new playerMap());
                modelBuilder.ApplyConfiguration(new CharacterMap());
            }

            // 使用种子数据
            modelBuilder.Entity<Player>().HasData(DataSeed.Players);
            modelBuilder.Entity<Character>().HasData(DataSeed.Characters);

        }
    }
}





