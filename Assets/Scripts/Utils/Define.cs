using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum Scenes
    {
        Unknown,
        MainScene,
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

    public enum UIEvent
    {
        Click,
        Drag,
        Press,
        PointerDown,
        PointerUp,
    }

    public enum SceneLocateObject
    {
        UnitSpawnSpot,
        MonsterSpawnSpot,
        MonsterCrystal,
    }

    public enum Layer
    {
        Floor = 7,
        Unit = 8,
        Monster = 9,
    }
}
