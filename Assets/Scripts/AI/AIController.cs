using UnityEngine;

public class AIController : Controller
{
    private float safeXPosition;
    public void Initialize(Vector3 positon, float moveSpeed)
    {
        playerTransform = transform;
        defaultMoveSpeed = moveSpeed;
        startPosition = positon;

        targetMoveSpeed = defaultMoveSpeed;
        xPos = startPosition.x;
        zPos = startPosition.z;
        safeXPosition = xPos;
        SetMaxXPosition(9);
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;

        if (!autoMoveForward)
        {
            Decelerate(deltaTime, 0.0f);
        }
        else if (targetMoveSpeed > moveSpeed)
        {
            Accelerate(deltaTime, targetMoveSpeed);
        }
        else if (targetMoveSpeed < moveSpeed)
        {
            Decelerate(deltaTime, targetMoveSpeed);
        }

        float speed = moveSpeed * deltaTime;

        zPos += speed;

        // X pozisyonunu güvenli pozisyona doðru yavaþça kaydýrma
        if (Mathf.Abs(playerTransform.position.x - safeXPosition) > 0.01f)
        {
            float horizontalSpeed = moveSpeed * horizontalSpeedFactor;
            float newX = Mathf.Lerp(transform.position.x, safeXPosition, horizontalSpeed * deltaTime);// moveSpeed * deltaTime); //move speed xMoveSpeed olarak deðiþecek //horizontalSpeedFactor yerine moveSpeed'di
            xPos = newX;
        }
        playerTransform.position = new Vector3(xPos, playerTransform.position.y, zPos);

        // Animasyon güncellemeleri
        if (animator != null && deltaTime > 0.0f)
        {
            float distanceTravelledSinceLastFrame = (playerTransform.position - lastPosition).magnitude;
            float distancePerSecond = distanceTravelledSinceLastFrame / deltaTime;

            animator.SetFloat("Speed", distancePerSecond);
        }

        if (playerTransform.position != lastPosition)
        {
            playerTransform.forward = Vector3.Lerp(playerTransform.forward, (playerTransform.position - lastPosition).normalized, speed);
        }
        lastPosition = playerTransform.position;
    }
    public void SetSafePosition(float position)
    {
        safeXPosition = position;
    }
}
