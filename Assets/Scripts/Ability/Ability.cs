using UnityEngine;
public abstract class Ability : ScriptableObject
{
    public string Name;
    public float AbilityDuration;
    public abstract void Activate(GameObject abilityOwner);
    public abstract void Deactivate();
}