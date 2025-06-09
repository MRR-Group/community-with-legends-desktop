using System;
using Domain.Entities;
using Domain.ValueObjects;
using Presentation.Utils;
namespace Presentation.Controls.DesignData;

public static class CommentDesignData
{
    public static Comment Data { get; } = new Comment()
    {
        Text = "I don't know. I'm not sure why myself. But if I were to take even one  step back, I believe that all those important oaths, promises and many  other deals 'til now, will all go to waste and I'll never be able to  return before you, ever again.",
        CreationDate = "3 days ago",
        User = new User(1, "Zoro", new Uri("https://cwl.purgal.xyz/avatars/1.png"), new Email("zoro@cwl.pl"), false, new Roles([]), new Permissions([]), new Date("2025-05-24T17:08:17.000000Z")),
    };
}
