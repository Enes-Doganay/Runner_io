using UnityEngine;

[CreateAssetMenu(menuName = "Ability/DirectUsableAbility/MoveSpeedAbility")]
public class MoveSpeedAbility : DirectUsableAbility
{
    [SerializeField] private float speedAmount;
    private GameObject owner;
    public override void Activate(GameObject owner)
    {
        this.owner = owner;
        owner.GetComponent<Controller>().AdjustSpeed(speedAmount);
    }
    public override void Deactivate()
    {
        owner.GetComponent<Controller>().ResetSpeed();
    }
}