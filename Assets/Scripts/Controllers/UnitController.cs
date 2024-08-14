using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : CharacterController
{
    protected override GameObject MainTarget()
    {
        return Managers.Game.MonsterCrystal;
    }

    protected override List<GameObject> Targets()
    {
        return Managers.Game.Monsters;
    }

    // 유닛은 4개의 사망 애니메이션이 있음
    protected override string DieAnimName() 
    {
        return $"Die{Random.Range(1, 5)}";
    }

    protected override int GetId()
    {
        return Util.GetNumToEnumName<CharacterConf.Unit>(gameObject.name);
    }

    protected override int GetLevel()
    {
        return Managers.Status.GetUnitLevel(GetId());
    }
}

