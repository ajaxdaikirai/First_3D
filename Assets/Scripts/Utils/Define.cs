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

    //public enum WorldObject
    //{
    //    Unknown,
    //    Player,
    //    Enemy,
    //}

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

    public enum Enemy
    {
        Enemy,
    }

    public enum Character
    {
        Character,
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
        Character = 8,
        Enemy = 9,
    }
}
