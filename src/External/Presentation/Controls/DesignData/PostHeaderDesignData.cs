using System;
using Presentation.Utils;
namespace Presentation.Controls.DesignData;

public static class PostHeaderDesignData
{
    public static PostHeader Data { get; } = new PostHeader()
    {
        UserName = "Zoro",
        UnderText = "Swordsman VR",
        CreationDate = "3 days ago",
        Avatar = ImageHelper.LoadFromWeb(new Uri("https://cwl.purgal.xyz/avatars/1.png")),
    };
}
