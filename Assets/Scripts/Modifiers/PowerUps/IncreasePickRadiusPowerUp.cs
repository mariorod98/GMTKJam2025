using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreasePickRadiusPowerUp : PowerUp
{
    public IncreasePickRadiusPowerUp()
    {
        m_type = PowerUpType.IncreasePickRadius;
    }

    public IncreasePickRadiusPowerUp(float value)
    {
        m_type = PowerUpType.IncreasePickRadius;
        m_floatModifier = value;
    }

    public override void ApplyModifier()
    {
        ModifierManager.Instance.m_pickRadiusModifier += m_floatModifier;
    }

    public override String GetFormattedText()
    {
        return "Make the pick radius " + (m_floatModifier * 100.0f).ToString("F0") + "% bigger";
    }
}
