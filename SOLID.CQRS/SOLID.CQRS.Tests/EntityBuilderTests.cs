using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.Extras.Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace SOLID.CQRS.Tests
{
    [TestClass]
    public class EntityBuilderTests
    {
        [TestMethod]
        public void ShouldReturnListOfEntitiesWhenSeededAndNoComponents()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                mock.Mock<IEntitySeed<IHasCustomerId>>().Setup(m => m.Execute())
                    .Returns(new IHasCustomerId[5]);

                mock.Provide<IEnumerable<IEntityComponent<IHasCustomerId>>>(new List<IEntityComponent<IHasCustomerId>>());

                var builder = mock.Create<EntityBuilder<IHasCustomerId>>();

                // Act
                var result = builder.Build();

                // Assert
                Assert.AreEqual(5, result.Count());
            }
        }

        [TestMethod]
        public void ShouldReturnListOfEntitiesWhenSeededAndHasComponents()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                const int listCount = 5;
                var mockSeed = mock.Mock<IEntitySeed<IHasCustomerId>>();
                mockSeed.Setup(m => m.Execute())
                    .Returns(new IHasCustomerId[listCount]);

                var mockComponent = mock.Mock<IEntityComponent<IHasCustomerId>>();
                mockComponent.Setup(m => m.Execute(mockSeed.Object.Execute()))
                    .Returns(new IHasCustomerId[listCount]);

                mock.Provide<IEnumerable<IEntityComponent<IHasCustomerId>>>(new List<IEntityComponent<IHasCustomerId>> {mockComponent.Object});

                var builder = mock.Create<EntityBuilder<IHasCustomerId>>();

                // Act
                var result = builder.Build();

                // Assert
                Assert.AreEqual(listCount, result.Count());
            }
        }

        [TestMethod]
        public void ShouldReturnEmptyListWhenNotSeeded()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                var builder = new EntityBuilder<IHasCustomerId>(null, null);

                // Act
                var result = builder.Build();

                // Assert
                Assert.AreEqual(0, result.Count());
            }
        }
    }
}
