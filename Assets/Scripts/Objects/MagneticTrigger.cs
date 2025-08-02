using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticTrigger : MonoBehaviour
{
    [SerializeField] private float m_maxForce = 20.0f;
    [SerializeField] private float m_maxRange = 10.0f;
    [SerializeField] private AnimationCurve m_forceFalloff;

    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        Vector3 direction = transform.parent.position - other.transform.position;
        float distance = direction.magnitude;

        if (distance > m_maxRange) return;

        float normalizedDistance = Mathf.Clamp01(distance / m_maxRange);
        float forceMultiplier = m_forceFalloff.Evaluate(normalizedDistance);

        Vector3 force = direction.normalized * m_maxForce * forceMultiplier;
        rb.AddForce(force);
    }
}

