using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseBowlSizePowerUp : PowerUp
{
    public IncreaseBowlSizePowerUp()
    {
        m_type = PowerUpType.IncreaseBowlSize;
    }

    public IncreaseBowlSizePowerUp(float value)
    {
        m_type = PowerUpType.IncreaseBowlSize;
        m_floatModifier = value;
    }

    public override void ApplyModifier()
    {
        //ModifierManager.Instance.m_timeModifier += m_floatModifier;
    }

    public override String GetFormattedText()
    {
        return "Increase bowl size " + (m_floatModifier * 100.0f).ToString("F0") + "%";
    }
}
