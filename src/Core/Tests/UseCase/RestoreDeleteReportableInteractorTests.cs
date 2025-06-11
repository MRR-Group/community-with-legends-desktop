using System;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.Exceptions;
using Application.UseCases;
using Domain.Entities;
using Domain.Primitives;
using Domain.ValueObjects;
using Moq;
using Xunit;

namespace Tests;

public class RestoreDeleteReportableInteractorTests
{
    [Fact]
    public async Task Restore_ShouldCallCommentService_WhenReportableIsComment()
    {
        var user = new User(1, "commenter", new Uri("https://example.com/avatar.jpg"), new Email("comment@example.com"), false, new Roles([]), new Permissions([]), new Date("2025-06-11T00:00:00Z"));
        var comment = new Comment(1, "content", new Date("2025-06-11T00:00:00Z"), user);

        var commentService = new Mock<ICommentService>();
        var postService = new Mock<IPostService>();

        commentService.Setup(s => s.Restore(comment)).Returns(Task.CompletedTask);

        var interactor = new RestoreDeleteReportableInteractor(postService.Object, commentService.Object);
        await interactor.Restore(comment);

        commentService.Verify(s => s.Restore(comment), Times.Once);
        postService.Verify(s => s.Restore(It.IsAny<Post>()), Times.Never);
    }

    [Fact]
    public async Task Restore_ShouldCallPostService_WhenReportableIsPost()
    {
        var user = new User(2, "poster", new Uri("https://example.com/avatar2.jpg"), new Email("post@example.com"), false, new Roles([]), new Permissions([]), new Date("2025-06-11T00:00:00Z"));
        var post = new Post(2, "post", new Date("2025-06-11T00:00:00Z"), user, null, [], null, 0, []);

        var commentService = new Mock<ICommentService>();
        var postService = new Mock<IPostService>();

        postService.Setup(s => s.Restore(post)).Returns(Task.CompletedTask);

        var interactor = new RestoreDeleteReportableInteractor(postService.Object, commentService.Object);
        await interactor.Restore(post);

        postService.Verify(s => s.Restore(post), Times.Once);
        commentService.Verify(s => s.Restore(It.IsAny<Comment>()), Times.Never);
    }

    [Fact]
    public async Task Restore_ShouldThrow_WhenUnsupportedReportableType()
    {
        var user = new User(3, "other", new Uri("https://example.com/avatar3.jpg"), new Email("other@example.com"), false, new Roles([]), new Permissions([]), new Date("2025-06-11T00:00:00Z"));
        var unknown = new FakeReportable(99, new Date("2025-06-11T00:00:00Z"), user, "unknown");

        var commentService = new Mock<ICommentService>();
        var postService = new Mock<IPostService>();

        var interactor = new RestoreDeleteReportableInteractor(postService.Object, commentService.Object);

        await Assert.ThrowsAsync<UnsupportedReportableTypeException>(() => interactor.Restore(unknown));
    }

    private class FakeReportable : Reportable
    {
        public FakeReportable(uint id, Date date, User user, string content) : base(id, date, user, content) { }
    }
}
