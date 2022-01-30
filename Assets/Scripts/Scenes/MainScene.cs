using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        _sceneType = Define.Scenes.MainScene;

        Managers.UI.ShowSceneUI<UISceneMain>();
    }

    public override void Clear()
    {
        
    }
}
