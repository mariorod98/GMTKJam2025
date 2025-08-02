using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseBonusTimePowerUp : PowerUp
{
    public IncreaseBonusTimePowerUp()
    {
        m_type = PowerUpType.IncreaseBowlSize;
    }

    public IncreaseBonusTimePowerUp(float value)
    {
        m_type = PowerUpType.IncreaseBowlSize;
        m_floatModifier = value;
    }

    public override void ApplyModifier()
    {
        ModifierManager.Instance.m_bonusCountdownModifier += m_floatModifier;
    }

    public override String GetFormattedText()
    {
        return "Increase bonus multiplier duration for " + m_floatModifier.ToString("F0") + " seconds";
    }
}
