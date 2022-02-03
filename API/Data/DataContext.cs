using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {

        //When this class is initiated, the constructor is called
        public DataContext(DbContextOptions options) : base (options) //Constructor
        {

        }


        //Creating a DataType DbSet of type AppUser from Entities
        // Users represents the table which we are going to create in DB
        public DbSet<AppUser> Users { get; set; }
    }


    //To Inject/Use the DataContext in other parts of application, add this DataContext to ConfigureServices in Startup.cs
}

