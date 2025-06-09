using Application.Abstractions;
using Application.Exceptions;
using Domain.Entities;
using Domain.Primitives;

namespace Application.UseCases;

public class RestoreDeleteReportableInteractor
{
    private IPostService _postService;
    private ICommentService _commentService;

    public RestoreDeleteReportableInteractor(IPostService postService, ICommentService commentService)
    {
        _postService = postService;
        _commentService = commentService;
    }

    public async Task Restore(Reportable reportable)
    {
        switch (reportable)
        {
            case Comment comment:
                await _commentService.Restore(comment);
                return;
            
            case Post post:
                await _postService.Restore(post);
                return;
            
            default:
                throw new UnsupportedReportableTypeException(reportable);
        }
    }
}