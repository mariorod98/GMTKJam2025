using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseBonusPowerUp : PowerUp
{
    public IncreaseBonusPowerUp()
    {
        m_type = PowerUpType.IncreaseBowlSize;
    }

    public IncreaseBonusPowerUp(int value)
    {
        m_type = PowerUpType.IncreaseBowlSize;
        m_intModifier = value;
    }

    public override void ApplyModifier()
    {
        ModifierManager.Instance.m_bonusScoreModifier += m_intModifier;
    }

    public override String GetFormattedText()
    {
        return "Add a x" + m_intModifier.ToString() + " to the bonus modifier";
    }
}
