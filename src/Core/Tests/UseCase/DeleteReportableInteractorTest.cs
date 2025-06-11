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

public class DeleteReportableInteractorTests
{
    [Fact]
    public async Task Delete_ShouldCallCommentService_WhenReportableIsComment()
    {
        var user = new User(1, "test", new Uri("https://example.com/avatar.jpg"), new Email("test@example.com"), false, new Roles([]), new Permissions([]), new Date("2025-06-11T10:15:28.000000Z"));
        var comment = new Comment(1, "content", new Date("2025-06-11T10:15:28.000000Z"), user);

        var postService = new Mock<IPostService>();
        var commentService = new Mock<ICommentService>();
        commentService.Setup(s => s.Delete(comment)).Returns(Task.CompletedTask);

        var interactor = new DeleteReportableInteractor(postService.Object, commentService.Object);
        await interactor.Delete(comment);

        commentService.Verify(s => s.Delete(comment), Times.Once);
        postService.Verify(s => s.Delete(It.IsAny<Post>()), Times.Never);
    }

    [Fact]
    public async Task Delete_ShouldCallPostService_WhenReportableIsPost()
    {
        var user = new User(2, "poster", new Uri("https://example.com/avatar2.jpg"), new Email("poster@example.com"), false, new Roles([]), new Permissions([]), new Date("2025-06-11T10:15:28.000000Z"));
        var post = new Post(
            1,
            "post content",
            new Date("2025-06-11T10:15:28.000000Z"),
            user,
            null,
            [],
            null,
            0,
            []
        );

        var postService = new Mock<IPostService>();
        var commentService = new Mock<ICommentService>();
        postService.Setup(s => s.Delete(post)).Returns(Task.CompletedTask);

        var interactor = new DeleteReportableInteractor(postService.Object, commentService.Object);
        await interactor.Delete(post);

        postService.Verify(s => s.Delete(post), Times.Once);
        commentService.Verify(s => s.Delete(It.IsAny<Comment>()), Times.Never);
    }

    [Fact]
    public async Task Delete_ShouldThrowUnsupportedReportableTypeException_WhenTypeIsUnknown()
    {
        var user = new User(3, "other", new Uri("https://example.com/avatar3.jpg"), new Email("other@example.com"), false, new Roles([]), new Permissions([]), new Date("2025-06-11T10:15:28.000000Z"));
        var unknown = new FakeReportable(99, new Date("2025-06-11T10:15:28.000000Z"), user, "unknown");

        var postService = new Mock<IPostService>();
        var commentService = new Mock<ICommentService>();

        var interactor = new DeleteReportableInteractor(postService.Object, commentService.Object);

        await Assert.ThrowsAsync<UnsupportedReportableTypeException>(() => interactor.Delete(unknown));
    }

    private class FakeReportable : Reportable
    {
        public FakeReportable(uint id, Date date, User user, string content) : base(id, date, user, content) { }
    }
}
