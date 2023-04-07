﻿using DomainModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DB : DbContext
    {
        public DbSet<TestObject> TestObjects { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DB(DbContextOptions<DB> options) : base(options) { }
    }
}
