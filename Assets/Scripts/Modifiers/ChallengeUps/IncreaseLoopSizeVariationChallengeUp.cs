using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseLoopSizeVariationChallengeUp : ChallengeUp
{
    public IncreaseLoopSizeVariationChallengeUp()
    {
        m_type = ChallengeUpType.IncreaseLoopSizeVariation;
    }

    public IncreaseLoopSizeVariationChallengeUp(float value)
    {
        m_type = ChallengeUpType.IncreaseLoopSizeVariation;
        m_floatModifier = value;
    }

    public override void ApplyModifier()
    {
        ModifierManager.Instance.m_loopSizeVariationModifier += m_floatModifier;
    }

    public override String GetFormattedText() 
    {
        return "Generate loops " + (m_floatModifier * 100.0f).ToString("F0") + "% bigger";
    }

}
