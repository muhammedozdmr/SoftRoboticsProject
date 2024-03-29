﻿using Microsoft.EntityFrameworkCore;
using SoftRobotics.DataAccess.Entity_Configurations;
using SoftRobotics.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftRobotics.DataAccess
{
    public class SoftRoboticsContext : DbContext
    {
        private const string ConnectionString = "Server = localhost; Database =  SoftRoboticsDb; User Id='.'; Password=''; Integrated Security = true;";

        public DbSet<RandomWord> RandomWords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RandomWordsConfigurations());
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}
