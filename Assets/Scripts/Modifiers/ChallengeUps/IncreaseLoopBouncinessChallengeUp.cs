using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseLoopBouncinessChallengeUp : ChallengeUp
{
    public IncreaseLoopBouncinessChallengeUp()
    {
        m_type = ChallengeUpType.IncreaseLoopBounciness;
    }

    public IncreaseLoopBouncinessChallengeUp(float value)
    {
        m_type = ChallengeUpType.IncreaseLoopBounciness;
        m_floatModifier = value;
    }

    public override void ApplyModifier()
    {
        ModifierManager.Instance.m_loopBouncinessModifier += m_floatModifier;
    }

    public override String GetFormattedText() 
    {
        return "Make loops bounce " + (m_floatModifier * 100.0f).ToString("F0") + "% more";
    }

}
