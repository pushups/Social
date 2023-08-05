using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Social.Models;

namespace Social.Data
{
    public class SocialContext : DbContext
    {
        public SocialContext (DbContextOptions<SocialContext> options)
            : base(options)
        {
        }

        public DbSet<Social.Models.Post> Post { get; set; } = default!;
    }
}