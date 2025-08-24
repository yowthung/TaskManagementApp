using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;
using FluentAssertions;
using Application.Tasks.Queries;
using Domain.Entities;
using Domain.Repositories;
public class GetAllTasksHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnTasks()
    {
        // Arrange
        var mockRepo = new Mock<ITaskRepository>();
        mockRepo.Setup(r => r.GetAllAsync(default))
                .ReturnsAsync(new List<TaskItem>
                {
                    new TaskItem { Id = 1, Title = "Test Task", Description = "Test Task 1", IsCompleted = false }
                });

        var handler = new GetAllTasksHandler(mockRepo.Object);

        // Act
        var result = await handler.Handle(new GetAllTasksQuery(), CancellationToken.None);

        // Assert
        result.Should().HaveCount(1);
        result[0].Title.Should().Be("Test Task");
    }
}
