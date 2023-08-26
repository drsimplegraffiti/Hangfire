using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FireApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FireApp.Data
{
   
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
    }
}