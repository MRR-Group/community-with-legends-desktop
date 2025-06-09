using Application.Abstractions;
using Application.Exceptions;
using Domain.Entities;
using Domain.Primitives;

namespace Application.UseCases;

public class DeleteReportableInteractor
{
    private IPostService _postService;
    private ICommentService _commentService;

    public DeleteReportableInteractor(IPostService postService, ICommentService commentService)
    {
        _postService = postService;
        _commentService = commentService;
    }

    public async Task Delete(Reportable reportable)
    {
        switch (reportable)
        {
            case Comment comment:
                await _commentService.Delete(comment);
                return;
            
            case Post post:
                await _postService.Delete(post);
                return;
            
            default:
                throw new UnsupportedReportableTypeException(reportable);
        }
    }
}