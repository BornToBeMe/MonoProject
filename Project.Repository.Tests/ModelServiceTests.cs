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
using X.PagedList;
using Xunit;

namespace Project.Service.Tests
{
    public class ModelServiceTests
    {
        [Fact]
        public async Task SelectAllAsync_Success_ReturnsCorrectResult()
        {
            var mockModelRepository = new Mock<IModelRepository>();
            var mockSorting = new Mock<ISorting>();
            var mockSearch = new Mock<ISearch>();
            var mockPaging = new Mock<IPaging>();
            var service = new ModelService(mockModelRepository.Object);
            var model = new List<Model.VehicleModel>
            {
                new Model.VehicleModel {
                    VehicleModelId = Guid.NewGuid(),
                    Name = "Car",
                    Abrv = "Car"
                },
                new Model.VehicleModel
                {
                    VehicleModelId = Guid.NewGuid(),
                    Name = "BMW",
                    Abrv = "BMW"
                }

            };

            mockModelRepository.Setup(ss => ss.SelectAsync(mockSorting.Object, mockSearch.Object, mockPaging.Object)).ReturnsAsync(model.ToPagedList());

            var actual = await service.SelectAsync(mockSorting.Object, mockSearch.Object, mockPaging.Object);
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
            actual.Should().NotBeNull();
        }

        [Fact]
        public async Task InsertAsync_Success_ReturnsTrue()
        {
            var mockModelRepository = new Mock<IModelRepository>();
            var service = new ModelService(mockModelRepository.Object);
            var model = new Model.VehicleModel
            {
                VehicleModelId = Guid.NewGuid(),
                Name = "Car",
                Abrv = "Car"
            };

            mockModelRepository.Setup(ss => ss.InsertAsync(model)).ReturnsAsync(true);
            var actual = await service.InsertAsync(model);
            actual.Should().BeTrue();
        }

        [Fact]
        public async Task InsertAsync_Success_ReturnsException()
        {
            var mockModelRepository = new Mock<IModelRepository>();
            var service = new ModelService(mockModelRepository.Object);
            var model = new Model.VehicleModel
            {
                VehicleModelId = Guid.NewGuid(),
                Name = "Car",
                Abrv = null
            };

            mockModelRepository.Setup(ss => ss.InsertAsync(model)).Throws<Exception>();
            Func<Task> action = async () => await service.InsertAsync(model);
            action.Should().Throw<Exception>();
        }

        [Fact]
        public async Task UpdateAsync_Success_ReturnsTrue()
        {
            var mockModelRepository = new Mock<IModelRepository>();
            var service = new ModelService(mockModelRepository.Object);
            var model = new Model.VehicleModel
            {
                VehicleModelId = Guid.NewGuid(),
                Name = "Car",
                Abrv = "Car"
            };

            mockModelRepository.Setup(ss => ss.UpdateAsync(model.VehicleModelId, model)).ReturnsAsync(true);
            var actual = await service.UpdateAsync(model.VehicleModelId, model);
            actual.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateAsync_Success_ReturnsException()
        {
            var mockModelRepository = new Mock<IModelRepository>();
            var service = new ModelService(mockModelRepository.Object);
            var model = new Model.VehicleModel
            {
                VehicleModelId = Guid.NewGuid(),
                Name = "Car",
                Abrv = null
            };

            mockModelRepository.Setup(ss => ss.UpdateAsync(model.VehicleModelId, model)).Throws<Exception>();
            Func<Task> action = async () => await service.UpdateAsync(model.VehicleModelId, model);
            action.Should().Throw<Exception>();
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

        [Fact]
        public async Task DeleteAsync_Success_ReturnsException()
        {
            var mockModelRepository = new Mock<IModelRepository>();
            var service = new ModelService(mockModelRepository.Object);
            var model = new Model.VehicleModel
            {
                Name = "Car",
                Abrv = "Car"
            };

            mockModelRepository.Setup(ss => ss.DeleteAsync(model.VehicleModelId)).Throws<Exception>();
            Func<Task> action = async () => await service.DeleteAsync(model.VehicleModelId);
            action.Should().Throw<Exception>();
        }
    }
}
