using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum Scenes
    {
        Unknown,
        MainScene,
        PrepareScene,
        GameScene,
    }

    public enum State
    {
        Die,
        Moving,
        Idle,
        Attack,
        Skill,
    }

    public enum SceneLocateObject
    {
        UnitSpawnSpot,
        MonsterSpawnSpot,
        MonsterCrystal,
    }

    public enum Layer
    {
        Unit = 7,
        Monster = 8,
        EnemyStaticObject = 9,
    }

    public enum Skill
    {
        Launch,
        Buff,
    }

    public const float DESPAWN_DELAY_TIME = 5.0f;

    // 첫 스테이지ID
    public const int FIRST_STAGE_ID = 1;
}
