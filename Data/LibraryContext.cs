using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moot.Models;

namespace Moot.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext (DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }

        public DbSet<Moot.Models.Property> Property { get; set; } = default!;
        public DbSet<Moot.Models.Client> Client { get; set; } = default!;
        public DbSet<Moot.Models.Neighborhood> Neighborhood { get; set; } = default!;
        public DbSet<Moot.Models.Owner> Owner { get; set; } = default!;
        public DbSet<Moot.Models.Offer> Offer { get; set; } = default!;
        public DbSet<Moot.Models.Agent> Agent { get; set; } = default!;
        public DbSet<Moot.Models.PublishedProperty> PublishedProperty { get; set; } = default!;
    }
}
