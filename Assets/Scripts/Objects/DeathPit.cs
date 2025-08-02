using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeathPit : MonoBehaviour
{
    public UnityEvent<GameObject> m_onLoopFell;

    private void OnTriggerEnter(Collider other)
    {
        m_onLoopFell.Invoke(other.gameObject);
    }
}
