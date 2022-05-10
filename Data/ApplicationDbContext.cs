using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Asm_web_1.Models;

namespace Asm_web_1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Asm_web_1.Models.Book> Book { get; set; }
        public DbSet<Asm_web_1.Models.usersaccounts> usersaccounts { get; set; }
        public DbSet<Asm_web_1.Models.orders> orders { get; set; }
        public DbSet<Asm_web_1.Models.report> report { get; set; }
    }
}
