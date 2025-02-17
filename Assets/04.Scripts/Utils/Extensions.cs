using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MondayCatWorld.Utils;

public static class Extensions
{
    public static string GetName(this Define.Scene scene)
    {
        return scene switch
        {
            Define.Scene.Title    => "TitleScene",
            Define.Scene.Lobby    => "LobbyScene",
            Define.Scene.TheStack => "TheStack",
            _                     => throw new ArgumentOutOfRangeException(nameof(scene), scene, null)
        };
    }
}
