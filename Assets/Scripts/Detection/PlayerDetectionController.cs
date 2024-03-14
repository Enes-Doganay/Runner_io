using UnityEngine;

public class PlayerDetectionController : DetectionController
{
    protected bool hasDetectedThreat = false;

    protected override void Start()
    {
        base.Start();
        enemyTag = "Enemy";
    }
    protected override void Update()
    {
        base.Update();
        RearThreatDetection();
    }
    protected virtual void RearThreatDetection()
    {
        hasDetectedThreat = Physics.BoxCast(col.bounds.center, boxCastSize / 2, -transform.forward, out hitInfo, transform.rotation, targetDetectionDistance, threatLayerMask);

        if (hasDetectedThreat)
        {
            if (hitInfo.transform.CompareTag("Skill"))
            {
                UIManager.Instance.ThreatDetected();
            }
        }
        else
        {
            UIManager.Instance.ThreatCleared();
        }
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        if (hasDetectedThreat)
        {
            Gizmos.color = Color.yellow;
        }
        else
        {
            Gizmos.color = Color.blue;
        }

        col = GetComponent<Collider>();
        boxCastSize = col.bounds.size;
        Gizmos.DrawCube(col.bounds.center - transform.forward * targetDetectionDistance / 2, new Vector3(boxCastSize.x, boxCastSize.y, targetDetectionDistance));
    }
}