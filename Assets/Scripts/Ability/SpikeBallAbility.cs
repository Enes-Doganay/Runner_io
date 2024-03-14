using UnityEngine;

[CreateAssetMenu(menuName = "Ability/EnemyTargetedAbility/SpikeBallAbility")]
public class SpikeBallAbility : EnemyTargetedAbility
{
    private GameObject ability;
    [SerializeField] private Vector3 abilityOffset = new Vector3(0, 0, 2f);
    public override void Activate(GameObject owner)
    {
        ability = Instantiate(abilityPrefab);
        ability.transform.position = owner.transform.position + abilityOffset;
        ability.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 100), ForceMode.Impulse);
    }
    public override void Deactivate()
    {
        Destroy(ability);
    }
}