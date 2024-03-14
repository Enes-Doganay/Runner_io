using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QualityManager : AbstractSingleton<QualityManager>
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
}
