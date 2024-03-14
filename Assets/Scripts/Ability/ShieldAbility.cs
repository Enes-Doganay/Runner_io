using UnityEngine;

[CreateAssetMenu(menuName = "Ability/DirectUsableAbility/ShieldAbility")]
public class ShieldAbility : DirectUsableAbility
{
    [SerializeField] private GameObject abilityPrefab;
    private GameObject abilityObject;
    public override void Activate(GameObject owner)
    {
        abilityObject = Instantiate(abilityPrefab, owner.transform.position, Quaternion.identity);
        abilityObject.transform.SetParent(owner.transform);
        //Instantiate shield
    }
    public override void Deactivate()
    {
        //destroy shield
        abilityObject.SetActive(false);
    }
}
