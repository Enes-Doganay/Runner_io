using UnityEngine;
using UnityEngine.InputSystem.HID;

public class AIDetectionController : DetectionController
{
    private bool hasDetectedObstacle = false;
    [SerializeField]
    private LayerMask obstacleLayerMask;

    protected override void Start()
    {
        base.Start();
        enemyTag = "Player";
    }
    protected override void Update()
    {
        base.Update();
        ObstacleDetection();
    }
    private void ObstacleDetection()
    {
        hasDetectedObstacle = Physics.BoxCast(col.bounds.center, boxCastSize / 2, transform.forward, out hitInfo, transform.rotation, targetDetectionDistance, obstacleLayerMask);

        if (hasDetectedObstacle)
        {
            if (hitInfo.transform.CompareTag("Obstacle"))
            {
                float safeXPosition = FindSafePosition();
                GetComponent<AIController>().SetSafePosition(safeXPosition);
            }
        }
    }

    private float FindSafePosition()
    {
        RaycastHit hit;
        float[] possiblePositions = { -3f, 0, 3f };
        float safePosition = transform.position.x;
        foreach(float position  in possiblePositions)
        {
            Vector3 origin = new Vector3(position, transform.position.y, transform.position.z);
            bool isObstacle = Physics.BoxCast(origin, boxCastSize / 2, transform.forward, out hit, transform.rotation, targetDetectionDistance, obstacleLayerMask);
            Debug.DrawLine(origin, origin + transform.forward * targetDetectionDistance, isObstacle ? Color.red : Color.green);

            if (!isObstacle)
            {
                safePosition = position;
                return safePosition;
            }
            Debug.DrawLine(origin, origin + transform.forward * targetDetectionDistance, Color.red);
        }
        return safePosition;
    }
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        RaycastHit hit;
        float[] possiblePositions = { -3f, 0, 3f };
        foreach (float position in possiblePositions)
        {
            Vector3 origin = new Vector3(position, transform.position.y, transform.position.z);
            bool isObstacle = Physics.BoxCast(origin, boxCastSize / 2, transform.forward, out hit, transform.rotation, targetDetectionDistance, obstacleLayerMask);

            // Draw the BoxCast as a line on the screen
            Gizmos.color = isObstacle ? Color.red : Color.green;
            Gizmos.DrawLine(origin, origin + transform.forward * targetDetectionDistance);
        }
    }
}