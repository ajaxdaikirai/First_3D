using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager
{
    int _stageId;

    public int StageId { get { return _stageId; } }

    public void Init()
    {
        _stageId = Define.FIRST_STAGE_ID;
    }
}
