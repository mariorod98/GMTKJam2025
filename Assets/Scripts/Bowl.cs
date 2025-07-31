using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bowl : MonoBehaviour
{
    [SerializeField] LoopColor m_loopColor;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Loop>().m_loopColor == m_loopColor)
        {
            GameManager.Instance.m_spawnManager.DespawnLoop(other.gameObject);
        }
    }
}
