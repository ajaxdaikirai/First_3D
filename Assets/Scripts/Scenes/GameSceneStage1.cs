using UnityEngine;

public class GameSceneStage1 : BaseScene
{
    GameScene_Panel start_Panel;
    //�����ϰ���� ���ͼ�
    int keep = 0;
    //���� �����ġ��� = ���ͼ�-1
    int monsters = 0;

    GameObject unit;
    GameObject go;
    UnitSpawningPool UnitSpawningPool;
    MonsterSpawningPool MonsterSpawningPool;

    protected override void Init()
    {
        base.Init();
        Managers.Game.Init();

        _sceneType = Define.Scenes.GameSceneStage1;

        Managers.UI.ShowSceneUI<UISceneGame>();

        //�� ����, �Ʊ� ������ ���� �Ŵ������� ��� ������ �� �ֵ��� ������ ����
        GameObject unit = new GameObject(name = "EnemySpawningPool");
        UnitSpawningPool UnitSpawningPool = Util.GetOrAddComponent<UnitSpawningPool>(unit);

        GameObject go = new GameObject() { name = "MonsterSpawningPool" };
        MonsterSpawningPool MonsterSpawningPool = Util.GetOrAddComponent<MonsterSpawningPool>(go);

        keep = 2;
        monsters = keep - 1;

        UnitSpawningPool.SetKeepObjectCount(keep);
        UnitSpawningPool.SetMaxUnitCount(keep * 2);
        MonsterSpawningPool.SetKeepMonsterCount(monsters);
        MonsterSpawningPool.SetMaxMonsterCount(keep*2);

        GameObject player = Managers.Game.InstantiatePlayer();

        //�� ������Ʈ �̸� ����
        gameObject.name = System.Enum.GetName(typeof(Define.Scenes), _sceneType);

        start_Panel = Managers.Game.StartPanel;
        start_Panel.Show();

        Managers.Game._summonedUnitCount = 0;
    }

    public void Update()
    {
    }
    public override void Clear()
    {
    }
}
