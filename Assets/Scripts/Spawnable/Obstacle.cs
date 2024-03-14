using UnityEngine;

public class Obstacle : Spawnable
{
    const string playerTag = "Player";
    private void OnTriggerEnter(Collider other)
    {
        ICharacterState characterState = other.GetComponent<ICharacterState>();
        if (characterState != null)
        {
            characterState.CharacterDeath();
        }
    }
}
