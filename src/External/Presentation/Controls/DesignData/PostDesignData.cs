using System;
using System.Collections.Generic;
using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;
using Presentation.Utils;
namespace Presentation.Controls.DesignData;

public static class PostDesignData
{
    public static Post Data { get; } = new Post()
    {
        Text = "I don't know. I'm not sure why myself. But if I were to take even one  step back, I believe that all those important oaths, promises and many  other deals 'til now, will all go to waste and I'll never be able to  return before you, ever again.",
        CreationDate = "3 days ago",
        Tags = new [] {"One piece", "Lifestyle"},
        Likes = "x 100",
        User = new User(1, "Zoro", new Uri("https://cwl.purgal.xyz/avatars/1.png"), new Email("zoro@cwl.pl"), false, new Roles([]), new Permissions([]), new Date("2025-05-24T17:08:17.000000Z")),
        Asset = new Asset(1, AssetType.Video, new Uri("http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4")),
    };
}
