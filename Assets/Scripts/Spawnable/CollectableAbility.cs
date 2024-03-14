using UnityEngine;

public class CollectableAbility : Collectable
{
    [SerializeField]
    private Ability ability;
    private void OnTriggerEnter(Collider other)
    {
        AbilityManager abilityManager = other.GetComponent<AbilityManager>();

        if (abilityManager != null)
        {
            abilityManager.AddAbility(ability, other.gameObject);
            if (other.tag == "Player")
                Collect();
            this.gameObject.SetActive(false);
        }
    }
    protected override void Collect()
    {
        base.Collect();
    }
}