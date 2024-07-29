using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawningPool : SpawningPool
{
    protected string DIRECTORY = "Monsters/";

    // 캐릭터 이름
    public string Name { get; set; }

    protected override Vector3 SpawnPos() { return Managers.Game.MonsterSpawnPos; }
    protected override string CharacterPath() { return DIRECTORY + Name; }
}
