using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseLoopsPerWaveChallengeUp : BaseModifier
{
    public IncreaseLoopsPerWaveChallengeUp()
    {
        m_type = ModifierType.ChallengeUp;
    }

    public IncreaseLoopsPerWaveChallengeUp(float value)
    {
        m_type = ModifierType.PowerUp;
        m_floatModifier = value;
    }

    public override void ApplyModifier()
    {
        ModifierManager.Instance.m_loopsMultiplierModifier += m_floatModifier;
    }

    public override String GetFormattedText() 
    { 
        return "Generate " + (m_floatModifier * 100.0f).ToString("F0") + "% more loops per round"; 
    }

}
