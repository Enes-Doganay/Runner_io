using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem confetti;

    [SerializeField]
    private SoundID confettiSound = SoundID.None;

    const string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        ICharacterState characterState = other.GetComponent<ICharacterState>();
        if(characterState != null)
        {
            characterState.CharacterVictory();
            confetti.Play();
        }

        if (other.CompareTag(playerTag))
        {
            UIManager.Instance.SetVictoryPanel();
            AudioManager.Instance.PlayEffect(confettiSound);
        }
    }
}