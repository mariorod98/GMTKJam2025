using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseNumberOfColorsChallengeUp : ChallengeUp
{
    public IncreaseNumberOfColorsChallengeUp()
    {
        m_type = ChallengeUpType.IncreaseNumberOfColors;
    }

    public IncreaseNumberOfColorsChallengeUp(int value)
    {
        m_type = ChallengeUpType.IncreaseNumberOfColors;
        m_intModifier = value;
    }

    public override void ApplyModifier()
    {
        ModifierManager.Instance.m_numberOfColorsModifier += m_intModifier;
    }

    public override String GetFormattedText() 
    { 
        return "Add a new color of loops"; 
    }

}
