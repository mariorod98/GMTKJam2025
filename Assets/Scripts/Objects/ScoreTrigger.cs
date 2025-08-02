using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreTrigger : MonoBehaviour
{
    [SerializeField] private LoopColor m_loopColor;
    public UnityEvent<Loop> m_onLoopEnteredBowl;
    public UnityEvent<Loop> m_onLoopExitedBowl;

    private void OnTriggerEnter(Collider other)
    {
        Loop loop = other.GetComponent<Loop>();
        if (loop.m_loopColor == m_loopColor)
        {
            m_onLoopEnteredBowl.Invoke(loop);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Loop loop = other.GetComponent<Loop>();
        if (loop.m_loopColor == m_loopColor)
        {
            m_onLoopExitedBowl.Invoke(loop);
        }
    }
}
