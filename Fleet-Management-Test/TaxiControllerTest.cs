using Fleet_Managment.Contex;
using Fleet_Managment.Controllers;
using Fleet_Managment.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Fleet_Management_Test
{
    [TestClass]
    public class TaxiControllerTest
    {
        private TaxiController _controller;
        private ContexBD _context;
        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ContexBD>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new ContexBD(options);
            // Seed the database with test data
            _context.Taxis.Add(new TaxiEntity { Id = 1, Plate = "ABC123" });
            _context.Taxis.Add(new TaxiEntity { Id = 2, Plate = "XYZ789" });
            _context.SaveChanges();
            _controller = new TaxiController(_context);
        }
        [TestMethod]
        public async Task GetTaxis_ReturnsCorrectData()
        {
            // Act
            var result = await _controller.GetTaxis();
            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var data = okResult.Value;
            Assert.IsNotNull(data);
            var totalCountProp = data.GetType().GetProperty("totalCount");
            Assert.IsNotNull(totalCountProp, "La propiedad 'totalCount' no está presente en el resultado.");
            int totalCount = (int)totalCountProp.GetValue(data);
            Assert.AreEqual(2, totalCount, "El número total de elementos no es el esperado.");
            var itemsProp = data.GetType().GetProperty("items");
            Assert.IsNotNull(itemsProp, "La propiedad 'items' no está presente en el resultado.");
            var items = (IEnumerable<object>)itemsProp.GetValue(data);
            Assert.AreEqual(2, items.Count(), "El número de elementos en 'items' no es el esperado.");
        }
        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}