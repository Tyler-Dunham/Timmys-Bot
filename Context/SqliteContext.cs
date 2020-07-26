using Discord_Bot.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Discord_Bot.Context
{
    public class SqliteContext : DbContext
    {
        public DbSet<Ban> Bans { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=Sqlite.db");
    }
}
