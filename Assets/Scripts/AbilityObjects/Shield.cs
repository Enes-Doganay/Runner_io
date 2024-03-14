using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField]
    private float forceValue;

    [SerializeField]
    private SoundID shieldSound = SoundID.None;

    private const string obstacleTag = "Obstacle";

    private void OnEnable()
    {
        AudioManager.Instance.PlayEffect(shieldSound);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(obstacleTag))
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                Rigidbody rb = contact.otherCollider.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                Vector3 forceDirection = contact.normal;
                ApplyForce(rb, -forceDirection);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(obstacleTag))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            Vector3 forceDirection = other.transform.position - transform.position;
            ApplyForce(rb, -forceDirection);
        }
    }
    private void ApplyForce(Rigidbody rb, Vector3 forceDirection)
    {
        if (rb != null)
        {
            Animator animator = rb.gameObject.GetComponent<Animator>();
            if (animator != null)
                animator.enabled = false;

            rb.AddForce(forceDirection * forceValue, ForceMode.Impulse);
        }
    }
}