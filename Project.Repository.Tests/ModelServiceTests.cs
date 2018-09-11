using FluentAssertions;
using Moq;
using Project.Common;
using Project.DAL;
using Project.Model;
using Project.Repository.Common;
using Project.Service;
using Project.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Project.Service.Tests
{
    public class ModelServiceTests
    {
        [Fact]
        public async Task SelectAllAsync_Success_ReturnsCorrectResult()
        {
            var mockModelRepository = new Mock<IModelRepository>();
            var service = new ModelService(mockModelRepository.Object);
            var model = new Model.VehicleModel
            {
                VehicleModelId = Guid.NewGuid(),
                Name = "Car",
                Abrv = "Car"
            };
        }

        [Fact]
        public async Task SelectByIDAsync_Success_ReturnsCorrectResult()
        {
            var mockModelRepository = new Mock<IModelRepository>();
            var service = new ModelService(mockModelRepository.Object);
            var model = new Model.VehicleModel {
                VehicleModelId = Guid.NewGuid(),
                Name = "Car",
                Abrv = "Car"
            };

            mockModelRepository.Setup(ss => ss.SelectByIDAsync(model.VehicleModelId)).ReturnsAsync(model);
            var actual = await service.SelectByIDAsync(model.VehicleModelId);
            // mockModelRepository.Setup(ss => ss.SelectByIDAsync(It.IsAny<Guid>())).ReturnsAsync(model);
            // var actual = await service.SelectByIDAsync(Guid.NewGuid());
            actual.Should().NotBeNull();
        }

        [Fact]
        public async Task CreateAsync_Success_ReturnsTrue()
        {
            var mockModelRepository = new Mock<IModelRepository>();
            var service = new ModelService(mockModelRepository.Object);
            var model = new Model.VehicleModel
            {
                VehicleModelId = Guid.NewGuid(),
                Name = "Car",
                Abrv = "Car"
            };

            mockModelRepository.Setup(ss => ss.CreateAsync(model)).ReturnsAsync(true);
            var actual = await service.CreateAsync(model);
            actual.Should().BeTrue();
        }

        [Fact]
        public async Task EditAsync_Success_ReturnsTrue()
        {
            var mockModelRepository = new Mock<IModelRepository>();
            var service = new ModelService(mockModelRepository.Object);
            var model = new Model.VehicleModel
            {
                VehicleModelId = Guid.NewGuid(),
                Name = "Car",
                Abrv = "Car"
            };

            mockModelRepository.Setup(ss => ss.EditAsync(model.VehicleModelId, model)).ReturnsAsync(true);
            var actual = await service.EditAsync(model.VehicleModelId, model);
            actual.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteAsync_Success_ReturnsTrue()
        {
            var mockModelRepository = new Mock<IModelRepository>();
            var service = new ModelService(mockModelRepository.Object);
            var model = new Model.VehicleModel
            {
                VehicleModelId = Guid.NewGuid(),
                Name = "Car",
                Abrv = "Car"
            };

            mockModelRepository.Setup(ss => ss.DeleteAsync(model.VehicleModelId)).ReturnsAsync(true);
            var actual = await service.DeleteAsync(model.VehicleModelId);
            actual.Should().BeTrue();
        }
    }
}
