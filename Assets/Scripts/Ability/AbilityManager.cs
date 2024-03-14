using System.Collections;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    private Ability ability;
    private GameObject abilityOwner;
    private void Start()
    {
        
    }
    public void AddAbility(Ability ability, GameObject owner)
    {
        // Add the ability to the collection
        this.ability = ability;
        abilityOwner = owner;
        
        if(owner.tag == "Player")
            UIManager.Instance.CollectedAbility(ability);

        if (ability is DirectUsableAbility directUsableAbility)
            UseAbility();
    }

    public void RemoveAbility()
    {
        // Remove the ability
        this.ability = null;
        UIManager.Instance.RemoveAbility();
    }

    public void UseAbility()
    {
        if (ability != null && this.enabled)
        {
            ability.Activate(abilityOwner);
            StartCoroutine(DeactivateAbilityAfterDelay(ability));
            RemoveAbility();
        }
    }
    private IEnumerator DeactivateAbilityAfterDelay(Ability ability)
    {
        yield return new WaitForSeconds(ability.AbilityDuration);
        ability.Deactivate();
    }
}