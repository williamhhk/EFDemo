using EFDemo.Controllers;
using EFDemo.Models;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace EFDemo.Tests
{

    [TestFixture]
    public class Test_No_Error_Valid
    {
        [Test]
        public void Test_Find_No_Error()
        {
            const int expectedId = 1;
            var expected = new TodoEntity { Id = expectedId, Description = "Entity 1" };
            var data = new List<TodoEntity>
            {
                expected,
                new TodoEntity { Id = 2, Description = "Entity 2" },
                new TodoEntity { Id = 3, Description = "Entity 3" },
                new TodoEntity { Id = 4, Description = "Entity 4" }
            }.AsQueryable();

            var dbSetMock = new Mock<IDbSet<TodoEntity>>();
            dbSetMock.Setup(m => m.Provider).Returns(data.Provider);
            dbSetMock.Setup(m => m.Expression).Returns(data.Expression);
            dbSetMock.Setup(m => m.ElementType).Returns(data.ElementType);
            dbSetMock.Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var customDbContextMock = new Mock<DemoDbContext>();
            customDbContextMock
                .Setup(x => x.DbEntities)
                .Returns(dbSetMock.Object);

            var classUnderTest = new TodoController(customDbContextMock.Object);

            //Action
            var actual = classUnderTest.Find(expectedId);

            //Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Description, actual.Description);

        }


        [Test]
        public void Test_Add_No_Error()
        {

            var entityToAdd = new TodoEntity { Id = 100, Description = "Entity 100" };
            var data = new List<TodoEntity>
            {
                new TodoEntity { Id = 1, Description = "Entity 1" }
            }.AsQueryable();

            var dbSetMock = new Mock<IDbSet<TodoEntity>>();
            dbSetMock.Setup(m => m.Provider).Returns(data.Provider);
            dbSetMock.Setup(m => m.Expression).Returns(data.Expression);
            dbSetMock.Setup(m => m.ElementType).Returns(data.ElementType);
            dbSetMock.Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var customDbContextMock = new Mock<DemoDbContext>();
            customDbContextMock.Setup(x => x.DbEntities).Returns(dbSetMock.Object);

            var classUnderTest = new TodoController(customDbContextMock.Object);

            //Action
            classUnderTest.Add(entityToAdd);
            dbSetMock.Verify(m => m.Add(It.IsAny<TodoEntity>()), Times.Once);
            customDbContextMock.Verify(m => m.SaveChanges(), Times.Once);
        }


        [Test]
        public void Test_Delete_No_Error()
        {

            var entityToDeleteId = 1;
            var data = new List<TodoEntity>
            {
                new TodoEntity { Id = 1, Description = "Entity 1" }
            }.AsQueryable();

            var dbSetMock = new Mock<IDbSet<TodoEntity>>();
            dbSetMock.Setup(m => m.Provider).Returns(data.Provider);
            dbSetMock.Setup(m => m.Expression).Returns(data.Expression);
            dbSetMock.Setup(m => m.ElementType).Returns(data.ElementType);
            dbSetMock.Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var customDbContextMock = new Mock<DemoDbContext>();
            customDbContextMock.Setup(x => x.DbEntities).Returns(dbSetMock.Object);

            var classUnderTest = new TodoController(customDbContextMock.Object);

            //Action
            classUnderTest.Delete(entityToDeleteId);
            dbSetMock.Verify(m => m.Remove(It.IsAny<TodoEntity>()), Times.Once);
            customDbContextMock.Verify(m => m.SaveChanges(), Times.Once);
        }

    }

}
