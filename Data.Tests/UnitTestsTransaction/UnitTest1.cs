using Data.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;

namespace Data.Tests.UnitTestsTransaction
{
    [TestClass]
    public class UnitTest1
    {
        private DbContextOptions<ApplicationDbContext> context;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Crear instancia del contexto de base de datos utilizando las opciones
            using (var dbContext = new ApplicationDbContext(options))
            {
                // Aplicar migraciones y asegurarse de que la base de datos está creada
                dbContext.Database.EnsureCreated();
            }

            // Utilizar las opciones para crear el contexto en las pruebas
            context = new ApplicationDbContext(options);

        }

        [TestMethod]
        public void GetTransactionByDate_ShouldReturntransactions()
        {
            //preparation


            //execution

            //verification
        }
    }
}