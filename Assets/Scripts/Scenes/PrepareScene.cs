using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        _sceneType = Define.Scenes.PrepareScene;

        Managers.UI.ShowSceneUI<UIScenePrepare>();
    }

    public override void Clear()
    {
        
    }
}
