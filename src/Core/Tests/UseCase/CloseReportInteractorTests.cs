using Application.Abstractions;
using Application.UseCases;
using Domain.Entities;
using Domain.Enums;
using Domain.Primitives;
using Domain.ValueObjects;
using Moq;

namespace Tests.UseCase;

public class CloseReportInteractorTests
{
    [Fact]
    public async Task Close_ShouldReturnTrue_WhenServiceReturnsTrue()
    {
        var report = CreateReport();
        var mockService = new Mock<IReportService>();
        mockService.Setup(s => s.Close(report)).ReturnsAsync(true);

        var interactor = new CloseReportInteractor(mockService.Object);

        var result = await interactor.Close(report);

        Assert.True(result);
        mockService.Verify(s => s.Close(report), Times.Once);
    }

    [Fact]
    public async Task Close_ShouldReturnFalse_WhenServiceReturnsFalse()
    {
        var report = CreateReport();
        var mockService = new Mock<IReportService>();
        mockService.Setup(s => s.Close(report)).ReturnsAsync(false);

        var interactor = new CloseReportInteractor(mockService.Object);

        var result = await interactor.Close(report);

        Assert.False(result);
        mockService.Verify(s => s.Close(report), Times.Once);
    }

    private Report CreateReport()
    {
        var user = new User(1, "user", new Uri("https://example.com/avatar.jpg"), new Email("user@example.com"), false, new Roles([]), new Permissions([]), new Date("2025-06-11T00:00:00Z"));
        var reportable = new FakeReportable(1, new Date("2025-06-11T00:00:00Z"), user, "content");
        return new Report(1, "reason", reportable, new Date("2025-06-11T00:00:00Z"), user, [ReportStatus.Pending], null);
    }

    private class FakeReportable(uint id, Date date, User user, string content)
        : Reportable(id, date, user, content);
}
