using UnityEngine;

public class DetectionController : MonoBehaviour
{
    protected Collider col;

    public LayerMask enemyLayerMask;
    public LayerMask threatLayerMask;

    protected RaycastHit hitInfo;   // allocating memory for the raycasthit
    protected AbilityManager abilityManager;

    protected Vector3 boxCastSize;

    [SerializeField]
    protected float targetDetectionDistance = 10f;
    
    protected bool hasDetectedEnemy = false;

    protected string enemyTag;
    
    protected virtual void Start()
    {
        col = GetComponent<Collider>();
        abilityManager = GetComponent<AbilityManager>();
        boxCastSize = col.bounds.size;
    }

    protected virtual void Update()
    {
        EnemyDetection();
    }

    protected virtual void EnemyDetection()
    {
        hasDetectedEnemy = Physics.BoxCast(col.bounds.center, boxCastSize / 2, transform.forward, out hitInfo, transform.rotation, targetDetectionDistance, enemyLayerMask); //new Vector3(mRaycastRadius * transform.localScale.x, mRaycastRadius, 0), transform.forward, out _mHitInfo, transform.rotation, mTargetDetectionDistance * transform.localScale.z, layerMask);

        if (hasDetectedEnemy)
        {
            abilityManager.UseAbility();
        }
    }
    protected virtual void OnDrawGizmos()
    {
        if (hasDetectedEnemy)
        {
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.green;
        }

        col = GetComponent<Collider>();
        boxCastSize = col.bounds.size;
        Gizmos.DrawCube(col.bounds.center + transform.forward * targetDetectionDistance / 2, new Vector3(boxCastSize.x, boxCastSize.y, targetDetectionDistance));
    }
}