using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        _sceneType = Define.Scenes.GameScene;

        Managers.UI.ShowSceneUI<UISceneGame>();

        // 몬스터 생산
        GameObject SpawningPool = new GameObject(name = "MonsterSpawningPool");
        Util.GetOrAddComponent<MonsterSpawningPool>(SpawningPool).SetKeepEnemyCount(6);

        // 씬 오브젝트 이름 변경
        gameObject.name = System.Enum.GetName(typeof(Define.Scenes), _sceneType);

        // 플레이어 생성
        GameObject player = Managers.Game.InstantiatePlayer();

        // 카메라 설정
        Util.GetOrAddComponent<CameraController>(Camera.main.gameObject).SetPlayer(player);
    }

    public override void Clear()
    {
    }
}
