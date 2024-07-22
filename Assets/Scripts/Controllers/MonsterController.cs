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

    protected override int Layer()
    {
        return (int)Define.Layer.Monster;
    }
}

