using System;
using ContactsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsAPI.Data
{
    public class ContactsAPIDbContext : DbContext
    {
        public ContactsAPIDbContext(DbContextOptions options) : base(options)
        {

        }

        // create table
        public DbSet<Contact> Contacts { get; set; }
    }
}

