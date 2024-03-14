using UnityEngine;

public class PlayerController : Controller
{
    private static PlayerController instance;
    public static PlayerController Instance => instance;

    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;

    private float targetPosition;
    public float TargetPosition => targetPosition;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        Initialize();
    }
    private void Initialize()
    {
        playerTransform = transform;
        startPosition = playerTransform.position;

        if(skinnedMeshRenderer != null)
        {
            playerHeight = skinnedMeshRenderer.bounds.size.y;
        }
        else
        {
            playerHeight = 1.0f;
        }

        ResetSpeed();
        SetMaxXPosition(9);
    }
    public Vector3 GetPlayerTop()
    {
        return playerTransform.position + Vector3.up * (playerHeight * playerTransform.localScale.y - playerHeight);
    }
    public void SetDeltaPosition(float normalizedDeltaPosition)
    {
        float fullWidth = maxXPosition * 2;
        targetPosition = targetPosition + fullWidth * normalizedDeltaPosition;
        targetPosition = Mathf.Clamp(targetPosition, -maxXPosition, maxXPosition);
        hasInput = true;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            animator.enabled = false;
            CancelMovement();
            InputManager.Instance.enabled = false;
        }

        float deltaTime = Time.deltaTime;

        //Update Speed
        if(!hasInput && !autoMoveForward)
        {
            Decelerate(deltaTime, 0.0f);
        }
        else if(targetMoveSpeed < moveSpeed)
        {
            Decelerate(deltaTime, targetMoveSpeed);
        }
        else if(targetMoveSpeed > moveSpeed)
        {
            Accelerate(deltaTime, targetMoveSpeed);
        }
        float speed = moveSpeed * deltaTime;

        //Update Position
        zPos += speed;

        if (hasInput)
        {
            float horizontalSpeed = speed * horizontalSpeedFactor;
            float newPositionTarget = Mathf.Lerp(xPos, targetPosition, horizontalSpeed);
            float newPositionDifference = newPositionTarget - xPos;

            newPositionDifference = Mathf.Clamp(newPositionDifference, -horizontalSpeed, horizontalSpeed);

            xPos += newPositionDifference;
        }
        playerTransform.position = new Vector3(xPos, playerTransform.position.y, zPos);


        if(animator != null && deltaTime > 0.0f)
        {
            float distanceTravelledSinceLastFrame = (playerTransform.position - lastPosition).magnitude;
            float distancePerSecond = distanceTravelledSinceLastFrame / deltaTime;

            animator.SetFloat("Speed", distancePerSecond);
        }

        if(playerTransform.position != lastPosition)
        {
            playerTransform.forward = Vector3.Lerp(playerTransform.forward, (playerTransform.position - lastPosition).normalized, speed);
        }
        lastPosition = playerTransform.position;
    }
}