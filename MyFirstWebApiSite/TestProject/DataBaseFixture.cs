using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class DatabaseFixture : IDisposable
    {
        public AdoNetUsers326077351Context Context { get; private set; }

        public DatabaseFixture()
        {
            // Set up the test database connection and initialize the context
            var options = new DbContextOptionsBuilder<AdoNetUsers326077351Context>()
                .UseSqlServer("Server=srv2\\pupils;Database=Tests_326077351;Trusted_Connection=True;TrustServerCertificate=true;")
                .Options;
            Context = new AdoNetUsers326077351Context(options);
            Context.Database.EnsureCreated();// create the data base
        }

        public void Dispose()
        {
            // Clean up the test database after all tests are completed
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}
