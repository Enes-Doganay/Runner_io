using UnityEngine;

public class PlayerState : MonoBehaviour, ICharacterState
{
    [SerializeField]
    private Animator animator;
    private PlayerController player;
    private bool invincible = false;

    private void Start()
    {
        player = GetComponent<PlayerController>();
        animator = GetComponentInChildren<Animator>();
    }
    private void OnEnable()
    {
        GameManager.GameStarted += CharacterInitialize;
    }

    public void CharacterInitialize()
    {
        player.enabled = true;
    }

    public void CharacterDeath()
    {
        if (!invincible)
        {
            GetComponentInChildren<Ragdoll>().ActivateRagdoll();
            animator.enabled = false;
            player.SetAutoMove(false);
            player.CancelMovement();
            InputManager.Instance.enabled = false;
            UIManager.Instance.SetGameOverPanel();
        }
    }

    public void CharacterVictory()
    {
        invincible = true;
        animator.SetTrigger("Victory");
        player.SetAutoMove(false);
        player.CancelMovement();
        InputManager.Instance.enabled = false;
    }
    private void OnDisable()
    {
        GameManager.GameStarted -= CharacterInitialize;
    }
}