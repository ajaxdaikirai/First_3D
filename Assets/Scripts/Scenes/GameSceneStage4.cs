using UnityEngine;

public class GameSceneStage4 : BaseScene
{
    GameScene_Panel start_Panel;
    //�����ϰ���� ���ͼ�
    int keep;
    //���� �����ġ��� = ���ͼ�-1
    int monsters;
    protected override void Init()
    {
        base.Init();
        Managers.Game.Init();

        _sceneType = Define.Scenes.GameSceneStage4;

        Managers.UI.ShowSceneUI<UISceneGame>();

        //�� ����, �Ʊ� ������ ���� �Ŵ������� ��� ������ �� �ֵ��� ������ ����
        GameObject unit = new GameObject(name = "EnemySpawningPool");
        UnitSpawningPool UnitSpawningPool = Util.GetOrAddComponent<UnitSpawningPool>(unit);
        GameObject go = new GameObject() { name = "MonsterSpawningPool" };
        MonsterSpawningPool MonsterSpawningPool = Util.GetOrAddComponent<MonsterSpawningPool>(go);
        keep = 5;
        monsters = keep - 1;
        UnitSpawningPool.SetKeepObjectCount(keep);
        UnitSpawningPool.SetMaxUnitCount(keep * 5);
        MonsterSpawningPool.SetKeepMonsterCount(monsters);
        MonsterSpawningPool.SetKeepMonsterCount(keep*5);

        GameObject player = Managers.Game.InstantiatePlayer();

        //�� ������Ʈ �̸� ����
        gameObject.name = System.Enum.GetName(typeof(Define.Scenes), _sceneType);

        start_Panel = Managers.Game.StartPanel;
        start_Panel.Show();

        Managers.Game._summonedUnitCount = 0;

    }

    public override void Clear()
    {
    }
}
