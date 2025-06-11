using Domain.Entities;
using Domain.Enums;
using Domain.Primitives;
using Domain.ValueObjects;

namespace Tests.Entities;

public class ReportTests
{
    [Fact]
    public void IsDeleted_ShouldReturnTrue_WhenStatusContainsDeleted()
    {
        var user = CreateUser();
        var reportable = CreateReportable(user);

        var report = new Report(1, "reason", reportable, new Date("2025-06-11T00:00:00Z"), user, [ReportStatus.Deleted], null);

        Assert.True(report.IsDeleted);
    }

    [Fact]
    public void IsDeleted_ShouldReturnFalse_WhenStatusDoesNotContainDeleted()
    {
        var user = CreateUser();
        var reportable = CreateReportable(user);

        var report = new Report(2, "reason", reportable, new Date("2025-06-11T00:00:00Z"), user, [ReportStatus.Pending], null);

        Assert.False(report.IsDeleted);
    }

    [Fact]
    public void IsResolved_ShouldReturnTrue_WhenStatusDoesNotContainPending()
    {
        var user = CreateUser();
        var reportable = CreateReportable(user);

        var report = new Report(3, "reason", reportable, new Date("2025-06-11T00:00:00Z"), user, [ReportStatus.Resolved], null);

        Assert.True(report.IsResolved);
    }

    [Fact]
    public void IsResolved_ShouldReturnFalse_WhenStatusContainsPending()
    {
        var user = CreateUser();
        var reportable = CreateReportable(user);

        var report = new Report(4, "reason", reportable, new Date("2025-06-11T00:00:00Z"), user, [ReportStatus.Pending], null);

        Assert.False(report.IsResolved);
    }

    [Fact]
    public void ContainsUserProfile_ShouldReturnTrue_WhenReportableIsUserProfile()
    {
        var user = new User(1, "username", new Uri("https://example.com/avatar.jpg"), new Email("user@example.com"), false, new Roles([]), new Permissions([]), new Date("2025-06-11T00:00:00Z"));

        var userProfile = new UserProfile(
            10,
            new Date("2025-06-11T00:00:00Z"),
            user,
            []
        );

        var report = new Report(5, "reason", userProfile, new Date("2025-06-11T00:00:00Z"), user, [ReportStatus.Pending], null);

        Assert.True(report.ContainsUserProfile);
    }

    [Fact]
    public void ContainsUserProfile_ShouldReturnFalse_WhenReportableIsNotUserProfile()
    {
        var user = CreateUser();
        var reportable = CreateReportable(user);

        var report = new Report(6, "reason", reportable, new Date("2025-06-11T00:00:00Z"), user, [ReportStatus.Pending], null);

        Assert.False(report.ContainsUserProfile);
    }

    private User CreateUser()
    {
        return new User(1, "user", new Uri("https://example.com/avatar.jpg"), new Email("user@example.com"), false, new Roles([]), new Permissions([]), new Date("2025-06-11T00:00:00Z"));
    }

    private Reportable CreateReportable(User user)
    {
        return new FakeReportable(1, new Date("2025-06-11T00:00:00Z"), user, "content");
    }

    private class FakeReportable(uint id, Date date, User user, string content)
        : Reportable(id, date, user, content);
}
