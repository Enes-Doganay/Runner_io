using UnityEngine;

public class AIState : MonoBehaviour, ICharacterState
{
    [SerializeField]
    private Animator animator;
    private Controller controller;
    private void Start()
    {
        controller = GetComponent<Controller>();
        animator = GetComponentInChildren<Animator>();
    }
    private void OnEnable()
    {
        GameManager.GameStarted += CharacterInitialize;
    }
    public void CharacterInitialize()
    {
        controller.enabled = true;
    }
    public void CharacterDeath()
    {
        GetComponentInChildren<Ragdoll>().ActivateRagdoll();
        animator.enabled = false;
        controller.SetAutoMove(false);
        controller.CancelMovement();
    }
    public void CharacterVictory()
    {
        animator.SetTrigger("Victory");
        controller.SetAutoMove(false);
        controller.CancelMovement();
    }
    private void OnDisable()
    {
        GameManager.GameStarted -= CharacterInitialize;
    }
}