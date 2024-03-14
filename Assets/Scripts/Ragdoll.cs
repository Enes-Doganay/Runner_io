using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    private Rigidbody[] rigidbodies;
    void Start()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        DeactivateRagdoll();
    }

    public void ActivateRagdoll()
    {
        foreach (var rigidbody in rigidbodies)
            rigidbody.isKinematic = false;
    }
    public void DeactivateRagdoll()
    {
        foreach(var rigidbody in rigidbodies)
            rigidbody.isKinematic = true;
    }
}
