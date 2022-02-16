using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticObjectController : MonoBehaviour
{
    void Start()
    {
        Init();
    }

    protected virtual void Init() {}
    public void Destroy()
    {
        Managers.Resource.Destroy(gameObject);
    }
}
