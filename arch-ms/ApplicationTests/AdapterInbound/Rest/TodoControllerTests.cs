using Application.AdapterInbound.Rest;
using Domain.Entities;
using Domain.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace ApplicationTests.AdapterInbound.Rest
{
    public class TodoControllerTests
    {

        private readonly Mock<TodoService> service = new();

        //[Fact]
        //public async Task When_HTTPGetTodo_Expect_Todo()
        //{
        //     //Arrange
        //    var controller = new TodoController(service.Object);

        //    // Act
        //    var actionResult = await controller.Get(service.Object, "1212");
        //    var result = actionResult.Result as OkObjectResult;

        //    // Assert
        //    Assert.NotNull(result);
        //}

        [Fact]
        public async Task GetById_withNonExistingTodo_ReturnsNotFound()
        {

            service.Setup(x => x.GetById(It.IsAny<string>()))
                .ReturnsAsync((Todo?)null);

            var controller = new TodoController(service.Object);

            var actionResult = await controller.Get(service.Object, "1212");

            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

      
        [Theory]
        [InlineData("a0184381704", true)]
        [InlineData("66766", false)]
        [InlineData("a0184381704", true)]
        public async Task GetById_withExistingTodo_ReturnsExpectedTodo(string id,bool test)
        {
            Todo todo = new Todo();

            service.Setup(x => x.GetById(It.IsAny<string>()))
                .ReturnsAsync(todo);

            var controller = new TodoController(service.Object);

            var actionResult = await controller.Get(service.Object, "1212");

            actionResult.Value.Should().BeEquivalentTo(todo,
                options => options.ComparingByMembers<Todo>()); // fluentAssertions
        }

    }
}
