using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawningPool : SpawningPool
{
    protected string PATH = "Monsters/EarthElemental";

    protected override Vector3 SpawnPos() { return Managers.Game.MonsterSpawnPos; }
    protected override string CharacterPath() { return PATH; }
    protected override int Layer() { return (int)Define.Layer.Monster; }
}
