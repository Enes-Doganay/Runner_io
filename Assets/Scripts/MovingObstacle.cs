using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    const string playerTag = "Player";
    private void OnTriggerEnter(Collider other)
    {
        ICharacterState characterState = other.GetComponent<ICharacterState>();
        if (characterState != null)
        {
            characterState.CharacterDeath();

            if (other.CompareTag(playerTag))
            {
                UIManager.Instance.SetGameOverPanel();
            }
        }
    }
}
