using AslMonitor.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AslMonitor.DAL;

public class DatabaseContext : DbContext
{

    //Commented to work with sqlite
    //public DatabaseContext(DbContextOptions<DatabaseContext> opts) : base(opts) { }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseSqlServer("Server=(local);Database=SchoolDB;Trusted_Connection=True");
    //}

    //For logging.
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        string fileLocation = "D:\\VS Repos\\AslMonitor\\AslMonitor\\TestDatabase.db";
        optionsBuilder.UseSqlite($"Filename={fileLocation}", options =>
        {
            options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
        });
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Unique key in UserInfos
        modelBuilder.Entity<UserInfo>()
            .HasIndex(u => new { u.UserID })
            .IsUnique(true);

        modelBuilder.Entity<UserInfo>()
            .HasIndex(u => new { u.EmailID })
            .IsUnique(true);
        modelBuilder.Entity<UserInfo>()
            .HasIndex(u => new { u.LoginID, })
            .IsUnique(true);
        modelBuilder.Entity<UserInfo>()
            .HasIndex(u => new { u.MobNo })
            .IsUnique(true);

        //Unique key in UserState
        modelBuilder.Entity<UserState>()
            .HasIndex(u => new { u.UserID })
            .IsUnique(true);

    }

    public DbSet<UserInfo> UserInfos => Set<UserInfo>();
    public DbSet<UserState> UserStates => Set<UserState>();
    public DbSet<CLog> CLogs => Set<CLog>();
    public DbSet<ScreenShot> ScreenShots => Set<ScreenShot>();
    public DbSet<LoginToken> LoginTokens => Set<LoginToken>();

}

