using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected PlayerSpeedPreset playerSpeed = PlayerSpeedPreset.Medium;
    [SerializeField] protected float customPlayerSpeed = 10.0f;
    [SerializeField] protected float accelerationSpeed = 10.0f;
    [SerializeField] protected float decelerationSpeed = 20.0f;
    [SerializeField] protected float horizontalSpeedFactor = 0.5f;
    [SerializeField] protected bool autoMoveForward = true;

    protected Transform playerTransform;
    protected Vector3 startPosition;
    protected Vector3 lastPosition;
    protected bool hasInput;
    protected float xPos;
    protected float zPos;
    protected float maxXPosition = 5f; //Level managerdan setleveldefination ile ayarlancak
    protected float defaultMoveSpeed;
    protected float moveSpeed;
    protected float targetMoveSpeed;
    protected float halftWidth = 0.5f;
    protected float playerHeight;

    public Transform Transform => playerTransform;
    public float MoveSpeed => moveSpeed;
    public float TargetMoveSpeed => targetMoveSpeed;
    public float MaxXPosition => maxXPosition;
    protected enum PlayerSpeedPreset
    {
        Slow,
        Medium,
        Fast,
        Custom
    }
    protected float GetDefaultMoveSpeed()
    {
        switch (playerSpeed)
        {
            case PlayerSpeedPreset.Slow:
                return 5.0f;
            case PlayerSpeedPreset.Medium:
                return 10.0f;
            case PlayerSpeedPreset.Fast:
                return 20.0f;
        }
        return defaultMoveSpeed;
    }
    public void AdjustSpeed(float speed)
    {
        targetMoveSpeed += speed;
        targetMoveSpeed = Mathf.Max(0, targetMoveSpeed);
    }
    public void ResetSpeed()
    {
        //moveSpeed = 0.0f;
        targetMoveSpeed = GetDefaultMoveSpeed();
    }
    public void SetMaxXPosition(float levelWidth)
    {
        maxXPosition = levelWidth * halftWidth;
    }
    protected virtual void Accelerate(float deltaTime, float targetSpeed)
    {
        moveSpeed += deltaTime * accelerationSpeed;
        moveSpeed = Mathf.Min(moveSpeed, targetSpeed);
    }
    protected virtual void Decelerate(float deltaTime, float targetSpeed)
    {
        moveSpeed -= deltaTime * decelerationSpeed;
        moveSpeed = Mathf.Max(moveSpeed, targetSpeed);
    }
    public void CancelMovement()
    {
        hasInput = false;
    }
    public void SetAutoMove(bool active)
    {
        autoMoveForward = active;
    }
    public void SetAnimator(bool active)
    {
        animator.enabled = active;
    }
}