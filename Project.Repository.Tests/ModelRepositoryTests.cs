using Moq;
using Project.DAL;
using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Project.Repository.Tests
{
    public class ModelRepositoryTests
    {
        [Fact]
        public void SelectByIDAsync_ShouldWork()
        {
            var mockDatabase = new Mock<ICarContext>();
            var model = new Model.VehicleModel();

            var mockModelRepository = new Mock<ModelRepository>();

            mockModelRepository.Setup(ss => ss.SelectByIDAsync(model.VehicleModelId)).ReturnsAsync(new Model.VehicleModel());
        }
    }
}
