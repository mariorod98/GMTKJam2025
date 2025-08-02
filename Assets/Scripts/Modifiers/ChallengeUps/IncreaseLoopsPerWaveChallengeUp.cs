using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseLoopsPerWaveChallengeUp : ChallengeUp
{
    public IncreaseLoopsPerWaveChallengeUp()
    {
        m_type = ChallengeUpType.IncreaseLoopsPerWave;
    }

    public IncreaseLoopsPerWaveChallengeUp(float value)
    {
        m_type = ChallengeUpType.IncreaseLoopsPerWave;
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
