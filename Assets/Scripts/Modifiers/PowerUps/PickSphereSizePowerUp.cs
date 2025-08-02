using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickSphereSizePowerUp : BaseModifier
{
    public PickSphereSizePowerUp()
    {
        m_type = ModifierType.PowerUp;
    }

    public PickSphereSizePowerUp(float value)
    {
        m_type = ModifierType.PowerUp;
        m_floatModifier = value;
    }

    public override void ApplyModifier()
    {
        ModifierManager.Instance.m_pickSphereSizeModifier += m_floatModifier;
    }

    public override String GetFormattedText()
    {
        return "Make the pick radius " + (m_floatModifier * 100.0f).ToString("F0") + "% bigger";
    }
}
