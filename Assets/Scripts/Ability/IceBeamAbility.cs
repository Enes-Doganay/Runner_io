using UnityEngine;

[CreateAssetMenu(menuName = "Ability/EnemyTargetedAbility/IceBeamAbility")]
public class IceBeamAbility : EnemyTargetedAbility
{
    private GameObject abilityObject;
    public override void Activate(GameObject owner)
    {
        abilityObject = Instantiate(abilityPrefab, owner.transform.position, Quaternion.identity);
        abilityObject.transform.SetParent(owner.transform);
    }
    public override void Deactivate()
    {
        abilityObject.SetActive(false);
    }
}