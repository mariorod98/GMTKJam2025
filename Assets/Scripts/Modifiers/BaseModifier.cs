using System;

public enum PowerUpType
{
    IncreasePickRadius,
    IncreaseTime,
    Size,
    IncreaseBowlSize,
    IncreaseBonusMultiplier,
    IncreaseBonusTime,
    IncreaseBowlMagnet,
}

public enum ChallengeUpType 
{ 
    IncreaseLoopsPerWave, 
    IncreaseNumberOfColors, 
    Size,
}

[Serializable]
public class BaseModifier
{
    public float m_floatModifier = 0.0f;
    public int m_intModifier = 0;

    public virtual String GetName() { return "BaseModifier"; }
    public virtual String GetFormattedText() { return ""; }
    public virtual void ApplyModifier() { }
}

[Serializable]
public class PowerUp : BaseModifier
{
    public PowerUpType m_type;
}

[Serializable]
public class ChallengeUp : BaseModifier
{
    public ChallengeUpType m_type;
}