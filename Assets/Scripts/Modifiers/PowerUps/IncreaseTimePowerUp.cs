using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseTimePowerUp : PowerUp
{
    public IncreaseTimePowerUp()
    {
        m_type = PowerUpType.IncreaseTime;
    }

    public IncreaseTimePowerUp(float value)
    {
        m_type = PowerUpType.IncreaseTime;
        m_floatModifier = value;
    }

    public override void ApplyModifier()
    {
        GameManager.Instance.IncreaseMaxCountdown(m_floatModifier);
    }

    public override String GetFormattedText()
    {
        return "Add " + m_floatModifier.ToString("F0") + " seconds to timer";
    }
}
