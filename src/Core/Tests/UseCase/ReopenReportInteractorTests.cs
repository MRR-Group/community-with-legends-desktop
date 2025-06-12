using System;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.UseCases;
using Domain.Entities;
using Domain.ValueObjects;
using Moq;
using Xunit;

namespace Tests.UseCase;

public class ReopenReportInteractorTests
{
    [Fact]
    public async Task Reopen_ShouldReturnTrue_WhenServiceSucceeds()
    {
        var user = new User(1, "user", new Uri("https://example.com/avatar.jpg"), new Email("user@example.com"), false, new Roles([]), new Permissions([]), new Date("2025-06-11T00:00:00Z"));
        var reportable = new FakeReportable(1, new Date("2025-06-11T00:00:00Z"), user, "content");
        var report = new Report(1, "reason", reportable, new Date("2025-06-11T00:00:00Z"), user, new[] { Domain.Enums.ReportStatus.Pending }, null);
        var mockService = new Mock<IReportService>();
        mockService.Setup(s => s.Reopen(report)).ReturnsAsync(true);
        var interactor = new ReopenReportInteractor(mockService.Object);
        var result = await interactor.Reopen(report);
        Assert.True(result);
        mockService.Verify(s => s.Reopen(report), Times.Once);
    }

    [Fact]
    public async Task Reopen_ShouldPropagateException_WhenServiceThrows()
    {
        var user = new User(1, "user", new Uri("https://example.com/avatar.jpg"), new Email("user@example.com"), false, new Roles([]), new Permissions([]), new Date("2025-06-11T00:00:00Z"));
        var reportable = new FakeReportable(1, new Date("2025-06-11T00:00:00Z"), user, "content");
        var report = new Report(1, "reason", reportable, new Date("2025-06-11T00:00:00Z"), user, new[] { Domain.Enums.ReportStatus.Pending }, null);
        var mockService = new Mock<IReportService>();
        mockService.Setup(s => s.Reopen(report)).ThrowsAsync(new Exception("Service error"));
        var interactor = new ReopenReportInteractor(mockService.Object);
        await Assert.ThrowsAsync<Exception>(() => interactor.Reopen(report));
    }

    private class FakeReportable : Domain.Primitives.Reportable
    {
        public FakeReportable(uint id, Date date, User user, string content) : base(id, date, user, content) { }
    }
}

