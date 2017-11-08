using System;
using System.Collections.Generic;
using Dierenasiel.Data;
using Dierenasiel.Models;
using Dierenasiel.Services;
using Moq;
using Xunit;

namespace Dierenasiel.Tests
{
    public class UnitTest1
    {
        private Mock<IAnimalService> mockForAnimalService = new Mock<IAnimalService>();

        [Fact]
        public void Test1()
        {
            var cat = new Cat {Name = "ksmdqfj"};
            Assert.Equal("ksmdqfj", cat.Name);
        }

        [Fact]
        public void Test2()
        {
            mockForAnimalService.Setup(x => x.GetAllOwnersSortedByName()).Returns(new List<Owner>()).Verifiable();
            
            //als je dit uit commentaar haalt faalt de test omdat de setup "getAllAnimalViewModels()" niet aangesproken wordt in de flow van "GetOwnersAsSelectListItems()"
            //mockForAnimalService.Setup(x => x.GetAllAnimalViewModels()).Returns(new List<OverviewAnimalViewModel>()).Verifiable();

            var translation = new TranslationService(mockForAnimalService.Object);
            var data = translation.GetOwnersAsSelectListItems();

            mockForAnimalService.Verify();

            Assert.Equal(1, data.Count);
        }
    }
}
