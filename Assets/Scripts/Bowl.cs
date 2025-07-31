using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Bowl : MonoBehaviour
{
    [SerializeField] private LoopColor m_loopColor;
    public UnityEvent<LoopColor> m_onLoopEnteredBowl;
    public UnityEvent<LoopColor> m_onLoopExitedBowl;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Loop>().m_loopColor == m_loopColor)
        {
            m_onLoopEnteredBowl.Invoke(m_loopColor);
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.GetComponent<Loop>().m_loopColor == m_loopColor)
        {
            m_onLoopExitedBowl.Invoke(m_loopColor);
        }
    }
}
