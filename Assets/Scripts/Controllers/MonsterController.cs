using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : CharacterController
{
    protected override GameObject MainTarget()
    {
        return Managers.Game.Player;
    }

    protected override List<GameObject> Targets()
    {
        return Managers.Game.Units;
    }

    protected override int GetId()
    {
        return Util.NumToEnumName<CharacterConf.Monster>(gameObject.name);
    }

    protected override int GetLevel()
    {
        data.StageSpawnMonster monster = Managers.Data.GetSpawnMonsterByStageIdAndMonsterId(Managers.Status.StageId, GetId());
        return monster.level;
    }
}

