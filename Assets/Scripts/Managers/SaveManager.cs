using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : AbstractSingleton<SaveManager>
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
}
