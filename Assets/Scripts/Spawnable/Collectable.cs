using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Spawnable
{
    [SerializeField]
    SoundID sound = SoundID.None;
    protected virtual void Collect()
    {
        AudioManager.Instance.PlayEffect(sound);
    }

}
